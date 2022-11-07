using System;

using LSWBackend.Dtos;

namespace LSWBackend.Services;

public class FreistellungsService
{

    private readonly LSWContext _db;
    public FreistellungsService(LSWContext db) => _db = db;

    public bool SetFreistellung(FreistellungsDto freistellungsDto)
    {
        Student student = _db.Students.SingleOrDefault(x => x.StudentId == freistellungsDto.studentId);

        if (student != null && student.StudentOffers.Count == 0)
        {
            foreach (int freistellungsId in freistellungsDto.freistellungsIdList)
            {
                Offer freistellung = _db.Offers.SingleOrDefault(x => x.OfferId == freistellungsId);

                _db.StudentOffers.Add(new StudentOffer
                {
                    OfferId = freistellung.OfferId,
                    Offer = freistellung,
                    StudentId = student.StudentId,
                    Student = student,
                });
                _db.SaveChanges();
            }
            return true;
        }
        else if (student != null)
        {
            foreach (int freistellungsId in freistellungsDto.freistellungsIdList)
            {
                Offer freistellung = _db.Offers.SingleOrDefault(x => x.OfferId == freistellungsId);

                foreach (StudentOffer offer in student.StudentOffers)
                {
                    foreach (OfferDate date in offer.Offer.OfferDates)
                    {
                        if (date.StartDate.Date == freistellung.OfferDates[0].StartDate.Date)
                        {
                            _db.StudentOffers.Remove(offer);
                            _db.SaveChanges();
                        }
                    }
                }
                _db.StudentOffers.Add(new StudentOffer
                {
                    OfferId = freistellung.OfferId,
                    Offer = freistellung,
                    StudentId = student.StudentId,
                    Student = student,
                });
                _db.SaveChanges();
            }
            return true;
        }
        return false;
    }
}

