using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CoursesService _service;

    public CoursesController(CoursesService service) => _service = service;

    [Authorize(Roles = "teacher")]
    [HttpPut("{id}")]
    public ActionResult<OfferDto> EditCourse(int id, [FromBody] CreateOfferDto offerPutDto) {
        int authorizedTeacherId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        if (!_service.IsTeachersCourse(id, authorizedTeacherId)) return Unauthorized($"Teacher with id {authorizedTeacherId} is not the owner of course {id}!");
        if (id < 0) return BadRequest("Id or request body incorrect");
        var replyOffer = _service.EditCourse(id, offerPutDto);
        return replyOffer.Description == "-1" ? (ActionResult<OfferDto>)NotFound("No offer with provided id found") :
            replyOffer.Description == "-2" ? (ActionResult<OfferDto>)BadRequest("Foreign key constraint error") :
            (ActionResult<OfferDto>)Ok(replyOffer);
    }

    [Authorize]
    [HttpGet("[action]")]
    public ActionResult<List<OfferDto>> GetTeacherCourses(int teacherId) {
        if (teacherId < 0) return BadRequest($"TeacherId {teacherId} not valid");
        var courses = _service.GetTeacherCourses(teacherId);
        return !courses.Any()
            ? (ActionResult<List<OfferDto>>)NotFound($"No courses for teacher with id {teacherId} found")
            : (ActionResult<List<OfferDto>>)Ok(courses);
    }

}
