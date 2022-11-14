using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly OffersService service;

        public OffersController(OffersService serv)
        {
            service = serv;
        }

        [HttpGet("GetOffers")]
        public IEnumerable<OfferDto> getOffers()
        {
            Console.WriteLine("Get Offers -- Getting all Offers");
            return service.getAllOffers();
        }

        [HttpDelete("DeleteOffer")]
        public ReplyDTO DeleteOffers(int id)
        {
            Console.WriteLine("Delete Offer - Deleting the Offer with the Id: " + id);
            return service.deleteOfferById(id);
        }

        [HttpPut("UpdateOffer")]
        public OfferDto UpdateOffer([FromBody] OfferDto offer)
        {
            Console.WriteLine("Update Offer - Updating Offer with Id: " + offer.OfferId);
            return service.updateOffer(offer);
        }

    }
}
