using System;
using LSWDbLib;

namespace LSWBackend.Services
{
    public class OffersService
    {
        private readonly LSWContext _db;

        public OffersService(LSWContext db)
        {
            this._db = db;
        }

        public IEnumerable<OfferDto> getAllOffers()
        {
            return _db.Offers.Include(x => x.Teacher).Include(y => y.OfferDates).Select(x => new OfferDto
            {
                OfferId = x.OfferId,
                TeacherId = x.TeacherId,
                Teacher = x.Teacher,
                offerDates = x.OfferDates,
                Title = x.Title
            }).ToList();
        }

        public ReplyDTO deleteOfferById(int id)
        {
            var reply = new ReplyDTO
            {
                isOK = true,
                ErrorMessage = ""
            };

            try
            {
                _db.Offers.Remove(_db.Offers.Single(x => x.OfferId == id));
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                reply.isOK = false;
                reply.ErrorMessage = e.Message;
            }

            return reply;
        }

        public OfferDto updateOffer(OfferDto dto)
        {
            var offer = _db.Offers.Single(x => x.OfferId == dto.OfferId);
            offer = new Offer().CopyPropertiesFrom(dto);
            _db.SaveChanges();
            return dto;

        }
    }
}