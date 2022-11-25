using System;

using LSWDbLib;

namespace LSWBackend.Services;

public class OffersService
{
    private readonly LSWContext _db;

    public OffersService(LSWContext db) => _db = db;

    public IEnumerable<OfferListDto> GetAllOffers() {
        return _db.Offers.Include(x => x.Teacher).Include(y => y.OfferDates).Select(x => new OfferListDto {
            OfferId = x.OfferId,
            TeacherId = x.TeacherId,
            Teacher = new TeacherDto().CopyPropertiesFrom(x.Teacher!),
            OfferDates = x.OfferDates,
            Title = x.Title
        }).ToList();
    }

    public ReplyDTO DeleteOfferById(int id) {
        var reply = new ReplyDTO {
            IsOK = true,
            ErrorMessage = ""
        };

        try {
            _db.Offers.Remove(_db.Offers.Single(x => x.OfferId == id));
            _db.SaveChanges();
        }
        catch (Exception e) {
            reply.IsOK = false;
            reply.ErrorMessage = e.Message;
        }

        return reply;
    }

    public OfferListDto UpdateOffer(OfferListDto dto) {
        var offer = _db.Offers.Single(x => x.OfferId == dto.OfferId);
        offer = new Offer().CopyPropertiesFrom(dto);
        _db.SaveChanges();
        return dto;
    }

    public bool CheckOfferFull(OfferListDto dto)
    {
        bool reply = false;
        int studentNum = _db.StudentOffers.Select(x => x.OfferId).Where(x => dto.OfferId == x).Sum();
        int minNum = _db.Offers.Single(x => x.OfferId == dto.OfferId).MinStudents;
        if(studentNum >= minNum)
        {
            SendEmailsService service = new SendEmailsService(new EmailSenderService());
            _db.StudentOffers.Include(x => x.Student).Include(x => x.Offer).Select(x => x).ToList().ForEach(x =>
            {
                service.SendNotificationCourseFailed($"{x.Student.Username}@sus.htl-grieskirchen.at", x.Offer.Title); 
            });
            reply = true;
        }
        return reply;
    }
}
