using System;

using LSWDbLib;

namespace LSWBackend.Services;

public class OffersService
{
    private readonly LSWContext _db;
    private readonly SendEmailsService _emailService;

    public OffersService(LSWContext db, SendEmailsService service) {
        _db = db;
        _emailService = service;
    }

    public IEnumerable<OfferSimpleDto> GetAllOffers() {
        return _db.Offers
            .Include(x => x.Teacher)
            .Include(y => y.OfferDates)
            .Select(x => new OfferSimpleDto {
                OfferId = x.OfferId,
                StartDate = x.OfferDates.Select(y => new OfferDateDto().CopyPropertiesFrom(y)).ToList()[0].StartDate.ToString(),
                EndDate = x.OfferDates.Select(y => new OfferDateDto().CopyPropertiesFrom(y)).ToList()[0].EndDate.ToString(),
                OfferName = x.Title,
                TeacherName = $"{x.Teacher!.FirstName} {x.Teacher!.LastName}"
            }).ToList();
    }

    public bool DeleteOfferById(int id) {
        try {
            _db.Offers.Remove(_db.Offers.Single(x => x.OfferId == id));
            _db.SaveChanges();
        }
        catch (Exception e) {
            return false;
        }

        return true;
    }

    public OfferSimpleDto UpdateOffer(OfferSimpleDto dto) {
        var offer = _db.Offers.Single(x => x.OfferId == dto.OfferId);
        offer = new Offer().CopyPropertiesFrom(dto);
        _db.SaveChanges();
        return dto;
    }

    public bool CheckOfferFull(OfferSimpleDto dto) {
        bool reply = false;
        int studentNum = _db.StudentOffers.Select(x => x.OfferId).Where(x => dto.OfferId == x).Sum();
        int minNum = _db.Offers.Single(x => x.OfferId == dto.OfferId).MinStudents;
        if (studentNum >= minNum) {
            _db.StudentOffers.Include(x => x.Student).Include(x => x.Offer).Select(x => x).ToList().ForEach(x =>
                _emailService.SendNotificationCourseFailed($"{x.Student.Username}@sus.htl-grieskirchen.at", x.Offer.Title));
            reply = true;
        }
        return reply;
    }

    public StudentOffer? AddStudentToOffer(int studentId, int offerId) {
        var offer = _db.Offers.Include(x => x.StudentOffers).Include(x => x.OfferDates).SingleOrDefault(x => x.OfferId == offerId);
        var student = _db.Students.Include(x => x.Clazz).Include(x => x.StudentOffers).ThenInclude(x => x.Offer).ThenInclude(x => x.OfferDates).SingleOrDefault(x => x.StudentId == studentId);
        if (student == null || offer == null) return new StudentOffer { OfferId = -1 }; //if student/offer doesn't exist
        if (offer.StudentOffers.Select(x => x.StudentId).Contains(studentId)) return null; //check if student is already in offer
        bool alreadyInCourse = false;
        student.StudentOffers.Select(x => x.Offer.OfferDates.Select(y => y.StartDate.Date)).ToList().ForEach(z => z.ToList().ForEach(xy => {
            if (offer.OfferDates.Select(yz => yz.StartDate.Date).Contains(xy)) alreadyInCourse = true;
        }));
        _db.ClassOffers.Where(x => x.ClazzId == student.ClazzId).Select(x => x.Offer.OfferDates.Select(y => y.StartDate.Date)).ToList().ForEach(z => z.ToList().ForEach(xy => {
            if (offer.OfferDates.Select(yz => yz.StartDate.Date).Contains(xy)) alreadyInCourse = true;
        }));
        if (alreadyInCourse) {
            return null;
        }
        else {
            _db.ClassOffers.Select(x => x.ClazzId).Contains(student.ClazzId);
        }
        var studentOffer = new StudentOffer { OfferId = offerId, StudentId = studentId };
        _db.StudentOffers.Add(studentOffer);
        _db.SaveChanges();
        return studentOffer;
    }

    public StudentOffer? RemoveStudentFromOffer(int studentId, int offerId) {
        var offer = _db.Offers.Include(x => x.StudentOffers).SingleOrDefault(x => x.OfferId == offerId);
        var student = _db.Students.Include(x => x.StudentOffers).SingleOrDefault(x => x.StudentId == studentId);
        if (student == null || offer == null) return null; //if student/offer doesn't exist
        var studentOffer = _db.StudentOffers.SingleOrDefault(x => x.StudentId == studentId && x.OfferId == offerId);
        try {
            _db.StudentOffers.Remove(studentOffer);
        }
        catch (ArgumentNullException) {
            return new StudentOffer { OfferId = -1 }; //Student does not exist in given offer
        }
        _db.SaveChanges();
        return studentOffer;
    }

    public void RemoveStudentFromOfferOnDay(int studentId, int offerId) {
        var offer = _db.Offers.Include(x => x.StudentOffers).Include(x => x.OfferDates).SingleOrDefault(x => x.OfferId == offerId);
        var student = _db.Students.Include(x => x.Clazz).Include(x => x.StudentOffers).ThenInclude(x => x.Offer).ThenInclude(x => x.OfferDates).SingleOrDefault(x => x.StudentId == studentId);
        if (student != null && offer != null) {

            student!.StudentOffers.ToList().ForEach(x => x.Offer.OfferDates.Select(y => y.StartDate.Date).ToList().ForEach(xy => {
                if (offer!.OfferDates.Select(xz => xz.StartDate.Date).Contains(xy)) _db.StudentOffers.Remove(x);
            }));
        }
        _db.SaveChanges();

    }
}
