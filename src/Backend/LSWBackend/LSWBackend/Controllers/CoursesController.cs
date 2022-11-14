using System.Security.Claims;

using Backend.Dtos;

using ChinookPlaylists;

using Microsoft.AspNetCore.Authorization;

namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly CoursesService _service;

    public CourseController(CoursesService service) => _service = service;

    [Authorize(Roles = "Teacher")]
    [HttpPut("{id}")]
    public ActionResult<OfferDto> EditCourse(int id, [FromBody] OfferDto offerDto) {
        int authorizedTeacherId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        if (authorizedTeacherId != offerDto.TeacherId) return Unauthorized($"Teacher with id {authorizedTeacherId} is not the owner of course {id}!");
        if (id < 0) return BadRequest("Id or request body incorrect");
        var replyOffer = _service.EditCourse(id, offerDto);
        return replyOffer == new Offer() ? (ActionResult<OfferDto>)NotFound("No offer with provided id found") : (ActionResult<OfferDto>)Ok(new OfferDto().CopyPropertiesFrom(replyOffer));
    }

    [Authorize]
    [HttpGet("[action]")]
    public ActionResult<List<OfferDto>> GetTeacherCourses(int teacherId) {
        if (teacherId < 0) return BadRequest($"TeacherId {teacherId} not valid");
        var courses = _service.GetTeacherCourses(teacherId);
        return !courses.Any()
            ? (ActionResult<List<OfferDto>>)NotFound($"No teacher with id {teacherId} found")
            : (ActionResult<List<OfferDto>>)Ok(courses
            .Select(x => new OfferDto().CopyPropertiesFrom(x))
            .ToList());
    }

}
