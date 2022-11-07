using System;

using LSWBackend.Dtos;

namespace LSWBackend.Services;

public class FreistellungsService
{

    private readonly LSWContext _db;
    public FreistellungsService(LSWContext db) => _db = db;

    public bool SetFreistellung(FreistellungsDto freistellungsDto) {
        Student student = _db.Students.SingleOrDefault(x => x.StudentId == freistellungsDto.studentId);

        if (student != null && student.StudentOffers.Count == 0) {
            foreach (int freistellungsId in freistellungsDto.freistellungsIdList) {
                Offer freistellung = _db.Offers.SingleOrDefault(x => x.OfferId == freistellungsId);

                _db.StudentOffers.Add(new StudentOffer {
                    OfferId = freistellung.OfferId,
                    Offer = freistellung,
                    StudentId = student.StudentId,
                    Student = student,
                });
                _db.SaveChanges();
            }
            return true;
        }
        else if (student != null) {
            foreach (int freistellungsId in freistellungsDto.freistellungsIdList) {
                foreach (StudentOffer offerId in student.StudentOffers) {
                    Offer freistellung = _db.Offers.SingleOrDefault(x => x.OfferId == freistellungsId);
                    if (freistellung != null) {

                    }
                }
                _db.StudentOffers.Add(new StudentOffer {
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

