using System;

using LSWBackend.Dtos;

namespace LSWBackend.Services;

public class FreistellungsService
{
    private readonly LSWContext _db;
    private readonly SendEmailsService _email;
    public FreistellungsService(LSWContext db, SendEmailsService email) {
        _db = db;
        _email = email;
    }

    public bool SetFreistellung(ExemptionDto freistellungsDto) {


        Student? student = _db.Students.Include(x => x.StudentOffers)
            .ThenInclude(x => x.Offer)
            .ThenInclude(x => x.OfferDates)
            .SingleOrDefault(x => x.StudentId == freistellungsDto.StudentId);
        Offer? freistellung = _db.Offers.Include(x => x.StudentOffers)
            .Include(x => x.OfferDates)
            .SingleOrDefault(x => x.OfferId == freistellungsDto.OfferId);
        if (freistellung != null && freistellung.VisibleForStudents == 0) {
            if (student != null && student.StudentOffers.Count == 0) {

                _db.StudentOffers.Add(new StudentOffer {
                    OfferId = freistellung.OfferId,
                    StudentId = student.StudentId,
                });
                _db.SaveChanges();
                _email.SendAbcence($"{student.Username}@sus.htl-grieskirchen.at", freistellung.OfferDates[0].StartDate.Date.ToString());
                return true;
            }
            else if (student != null) {
                var offersToRemove = new List<StudentOffer>();
                foreach (StudentOffer offer in student.StudentOffers) {
                    foreach (OfferDate date in offer.Offer.OfferDates) {
                        if (date.StartDate.Date == freistellung.OfferDates[0].StartDate.Date) {
                            offersToRemove.Add(offer);
                        }
                    }
                }
                _db.StudentOffers.RemoveRange(offersToRemove);
                _db.SaveChanges();
                _db.StudentOffers.Add(new StudentOffer {
                    OfferId = freistellung.OfferId,
                    StudentId = student.StudentId,
                });
                _db.SaveChanges();
                _email.SendAbcence($"{student.Username}@sus.htl-grieskirchen.at", freistellung.OfferDates[0].StartDate.Date.ToString());
                return true;
            }
        }
        return false;
    }
}

