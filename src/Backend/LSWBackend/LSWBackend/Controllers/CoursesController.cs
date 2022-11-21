namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly CoursesService _service;

    public CoursesController(CoursesService service) => _service = service;

    [Authorize(Roles = "teacher")]
    [HttpPut("{id}")]
    public ActionResult<OfferPutReplyDto> EditCourse(int id, [FromBody] OfferPutDto offerPutDto) {
        this.Log();
        int authorizedTeacherId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        if (!_service.IsTeachersCourse(id, authorizedTeacherId)) return Unauthorized($"Teacher with id {authorizedTeacherId} is not the owner of course {id}!");
        if (id < 0) return BadRequest("Id or request body incorrect");
        var replyOffer = _service.EditCourse(id, offerPutDto);
        return replyOffer.OfferId == -1 ? (ActionResult<OfferPutReplyDto>)NotFound("No offer with provided id found") :
            replyOffer.OfferId == -2 ? (ActionResult<OfferPutReplyDto>)BadRequest("Foreign key constraint error") :
            (ActionResult<OfferPutReplyDto>)Ok(new OfferPutReplyDto().CopyPropertiesFrom(replyOffer));
    }

    [Authorize]
    [HttpGet("[action]")]
    public ActionResult<List<OfferDto>> GetTeacherCourses(int teacherId) {
        this.Log();
        if (teacherId < 0) return BadRequest($"TeacherId {teacherId} not valid");
        var courses = _service.GetTeacherCourses(teacherId);
        return !courses.Any()
            ? (ActionResult<List<OfferDto>>)NotFound($"No teacher with id {teacherId} found")
            : (ActionResult<List<OfferDto>>)Ok(courses);
    }

}
