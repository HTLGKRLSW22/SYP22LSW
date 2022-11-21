using System;

using LSWBackend.Dtos;

namespace LSWBackend.Services;

public class FreistellungsService
{

    private readonly LSWContext _db;
    public FreistellungsService(LSWContext db) => _db = db;

    public bool SetFreistellung(FreistellungsDto freistellungsDto) {
        Student? student = _db.Students.Include(x => x.StudentOffers)
            .ThenInclude(x => x.Offer)
            .ThenInclude(x => x.OfferDates)
            .SingleOrDefault(x => x.StudentId == freistellungsDto.StudentId);
        Offer? freistellung = _db.Offers.Include(x => x.StudentOffers)
            .Include(x => x.OfferDates)
            .SingleOrDefault(x => x.OfferId == freistellungsDto.FreistellungsId);
        if (freistellung != null && freistellung.VisibleForStudents == 0) {
            if (student != null && student.StudentOffers.Count == 0) {

                _db.StudentOffers.Add(new StudentOffer {
                    OfferId = freistellung.OfferId,
                    StudentId = student.StudentId,
                });
                _db.SaveChanges();
                return true;
            }
            else if (student != null) {
                foreach (StudentOffer offer in student.StudentOffers) {
                    foreach (OfferDate date in offer.Offer.OfferDates) {
                        if (date.StartDate.Date == freistellung.OfferDates[0].StartDate.Date) {
                            _db.StudentOffers.Remove(offer);
                            _db.SaveChanges();
                        }
                    }
                }
                _db.StudentOffers.Add(new StudentOffer {
                    OfferId = freistellung.OfferId,
                    StudentId = student.StudentId,
                });
                _db.SaveChanges();
                return true;
            }
        }
        return false;
    }
}

