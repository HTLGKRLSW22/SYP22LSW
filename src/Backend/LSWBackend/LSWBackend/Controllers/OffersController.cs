using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

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

    [Authorize(Roles = "student, admin")]
    [HttpPut("[action]")]
    public ActionResult AddStudentToOffer(int studentId, int offerId) {

        bool isStudent = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "student";
        if (isStudent) {
            int isStudentId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (isStudentId != studentId) { //Student kann nur sich selber in Kurs hinzufügen keine Mitschüler
                return BadRequest("A student can not add another student to a course, only himself!");
            }
        }

        bool isAdmin = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "admin";
        if (isAdmin) {
            _service.RemoveStudentFromOfferOnDay(studentId, offerId);
        }

        if (studentId < 0) return BadRequest("studentId is not a valid input");
        if (offerId < 0) return BadRequest("offerId is not a valid input");
        var replyOffer = _service.AddStudentToOffer(studentId, offerId);
        return replyOffer == null
            ? BadRequest("Student does already exist in offer")
            : replyOffer.OfferId == -1
            ? BadRequest("No offer/student with provided id found")
            : Ok();
        //var person = new { Name = "John", Age = 30 };
    }

    [Authorize(Roles = "student, admin")]
    [HttpPut("[action]")]
    public ActionResult RemoveStudentFromOffer(int studentId, int offerId) {
        if (studentId < 0) return BadRequest("studentId is not a valid input");
        if (offerId < 0) return BadRequest("offerId is not a valid input");
        bool isStudent = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "student";
        if (isStudent) {
            int isStudentId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (isStudentId != studentId) { //Student kann nur sich selber von Kurs entfernen, keine Mitschüler
                return BadRequest("A student can not remove another student from a course, only himself!");
            }
        }
        var replyOffer = _service.RemoveStudentFromOffer(studentId, offerId);
        return replyOffer == null
            ? BadRequest("No offer/student with provided id found")
            : replyOffer.OfferId == -1
            ? BadRequest("Student does not exist in given offer")
            : Ok();
    }

}
