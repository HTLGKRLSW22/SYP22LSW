namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class OffersController : ControllerBase
{
    private readonly OffersService _service;

    public OffersController(OffersService serv) => _service = serv;

    [HttpGet("GetOffers")]
    public IEnumerable<OfferListDto> GetOffers() {
        Console.WriteLine("Get Offers -- Getting all Offers");
        return _service.GetAllOffers();
    }

    [HttpDelete("DeleteOffer")]
    public ReplyDTO DeleteOffers(int id) {
        Console.WriteLine("Delete Offer - Deleting the Offer with the Id: " + id);
        return _service.DeleteOfferById(id);
    }

    [HttpPut("UpdateOffer")]
    public OfferListDto UpdateOffer([FromBody] OfferListDto offer) {
        Console.WriteLine("Update Offer - Updating Offer with Id: " + offer.OfferId);
        return _service.UpdateOffer(offer);
    }

}
