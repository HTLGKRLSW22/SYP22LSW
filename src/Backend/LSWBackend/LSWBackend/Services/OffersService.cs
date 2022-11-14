using System;

using LSWDbLib;

namespace LSWBackend.Services;

public class OffersService
{
    private readonly LSWContext _db;

    public OffersService(LSWContext db) => _db = db;

    public IEnumerable<OfferDto> GetAllOffers() {
        return _db.Offers.Include(x => x.Teacher).Include(y => y.OfferDates).Select(x => new OfferDto {
            OfferId = x.OfferId,
            TeacherId = x.TeacherId,
            Teacher = x.Teacher,
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

    public OfferDto UpdateOffer(OfferDto dto) {
        var offer = _db.Offers.Single(x => x.OfferId == dto.OfferId);
        offer = new Offer().CopyPropertiesFrom(dto);
        _db.SaveChanges();
        return dto;

    }
}
