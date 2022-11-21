namespace LSWBackend.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class WaitingListsController : ControllerBase
{
    private readonly WaitingListsService _service;

    public WaitingListsController(WaitingListsService service) => _service = service;

    [Authorize(Roles = "admin,student")]
    [HttpPut]
    public ActionResult<WaitingListDto> RemoveStudent(int waitingListId) {
        this.Log();
        if (waitingListId < 0) return BadRequest("WaitingListId incorrect!");
        bool isStudent = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "Student";
        int studentId = -1;
        if (isStudent) studentId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var removedWaitingList = isStudent ? _service.RemoveStudentFromWaitingList(waitingListId, studentId) : _service.RemoveStudentFromWaitingList(waitingListId);
        return removedWaitingList == null
            ? (ActionResult<WaitingListDto>)Unauthorized("Student is not on waiting list!")
            : removedWaitingList.WaitingListId == -1
            ? (ActionResult<WaitingListDto>)NotFound("WaitingList not found")
            : (ActionResult<WaitingListDto>)Ok(new WaitingListDto().CopyPropertiesFrom(removedWaitingList));
    }

    [Authorize(Roles = "admin,student")]
    [HttpPost]
    public ActionResult<WaitingListDto> AddStudent(int studentId, int offerId) {
        this.Log();
        if (studentId < 0 || offerId < 0) return BadRequest("StudentId or OfferId incorrect!");
        bool isStudent = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "Student";
        if (isStudent) {
            int authorizedStudentId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            Console.WriteLine($"authorizedStudentId {authorizedStudentId}");
            if (authorizedStudentId != studentId) return Unauthorized($"Calling student with id {authorizedStudentId} is not equal to parameter student id {studentId}!");
        }
        var waitingList = _service.AddStudentToWaitingList(studentId, offerId);
        return waitingList.StudentId == -1 || waitingList.OfferId == -1
            ? (ActionResult<WaitingListDto>)NotFound("No student or no offer found with given ids!")
            : (ActionResult<WaitingListDto>)Ok(new WaitingListDto().CopyPropertiesFrom(waitingList));
    }

}
