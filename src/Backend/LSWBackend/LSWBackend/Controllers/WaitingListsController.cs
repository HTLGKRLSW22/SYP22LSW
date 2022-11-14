using System.Security.Claims;

using Backend.Dtos;

using ChinookPlaylists;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class WaitingListController : ControllerBase
{
    private readonly WaitingListsService _service;

    public WaitingListController(WaitingListsService service) => _service = service;

    [Authorize("Admin,Student")]
    [HttpPut]
    public ActionResult<WaitingListDto> RemoveStudent(int waitingListId) {
        if (waitingListId < 0) return BadRequest("WaitingListId incorrect!");
        bool isStudent = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "Student";
        int studentId = -1;
        if (isStudent) studentId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        var removedWaitingList = isStudent ? _service.RemoveStudentFromWaitingList(waitingListId, studentId) : _service.RemoveStudentFromWaitingList(waitingListId);
        return removedWaitingList == null
            ? (ActionResult<WaitingListDto>)Unauthorized("Student is not on waiting list!")
            : removedWaitingList == new WaitingList()
            ? (ActionResult<WaitingListDto>)NotFound("WaitingList not found")
            : (ActionResult<WaitingListDto>)Ok(new WaitingListDto().CopyPropertiesFrom(removedWaitingList));
    }

    [Authorize("Admin,Student")]
    [HttpPost]
    public ActionResult<WaitingListDto> AddStudent(int studentId, int offerId) {
        if (studentId < 0 || offerId < 0) return BadRequest("StudentId or OfferId incorrect!");
        bool isStudent = User.Claims.Single(x => x.Type == ClaimTypes.Role).Value == "Student";
        if (isStudent) {
            int authorizedStudentId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (authorizedStudentId != studentId) return Unauthorized($"Calling student with id {authorizedStudentId} is not equal to parameter student id {studentId}!");
        }
        var waitingList = _service.AddStudentToWaitingList(studentId, offerId);
        return waitingList == new WaitingList()
            ? (ActionResult<WaitingListDto>)NotFound("No student or no offer found with given ids!")
            : (ActionResult<WaitingListDto>)Ok(new WaitingListDto().CopyPropertiesFrom(waitingList));
    }

}
