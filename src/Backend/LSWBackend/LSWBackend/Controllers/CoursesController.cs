namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CoursesService _service;

    public CoursesController(CoursesService service) => _service = service;

    [Authorize(Roles = "teacher")]
    [HttpPut("{id}")]
    public ActionResult<OfferDetailDto> EditCourse(int id, [FromBody] OfferPutDto offerPutDto) {
        int authorizedTeacherId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        if (!_service.IsTeachersCourse(id, authorizedTeacherId)) return Unauthorized($"Teacher with id {authorizedTeacherId} is not the owner of course {id}!");
        if (id < 0) return BadRequest("Id or request body incorrect");
        var replyOffer = _service.EditCourse(id, offerPutDto);
        return replyOffer.OfferId == -1 ? (ActionResult<OfferDetailDto>)NotFound("No offer with provided id found") :
            replyOffer.OfferId == -2 ? (ActionResult<OfferDetailDto>)BadRequest("Foreign key constraint error") :
            (ActionResult<OfferDetailDto>)Ok(replyOffer);
    }

    [Authorize]
    [HttpGet("[action]")]
    public ActionResult<List<OfferDetailDto>> GetTeacherCourses(int teacherId) {
        if (teacherId < 0) return BadRequest($"TeacherId {teacherId} not valid");
        var courses = _service.GetTeacherCourses(teacherId);
        return !courses.Any()
            ? (ActionResult<List<OfferDetailDto>>)NotFound($"No courses for teacher with id {teacherId} found")
            : (ActionResult<List<OfferDetailDto>>)Ok(courses);
    }

}
