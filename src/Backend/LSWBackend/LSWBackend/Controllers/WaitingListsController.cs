using Backend.Dtos;
using ChinookPlaylists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WaitingListController : ControllerBase
    {
        private readonly WaitingListsService _service;

        public WaitingListController(WaitingListsService service) => _service = service;

        [HttpPut]
        public ActionResult<WaitingListDto> RemoveStudent(int waitingListId)
        {
            if (waitingListId < 0) return BadRequest("WaitingListId incorrect!");
            var removedWaitingList = _service.RemoveStudentFromWaitingList(waitingListId);
            if (removedWaitingList == null) return NotFound();
            return Ok(new WaitingListDto().CopyPropertiesFrom(removedWaitingList));
        }

        [HttpPost]
        public ActionResult<WaitingListDto> AddStudent(int studentId, int offerId)
        {
            if (studentId < 0 || offerId < 0) return BadRequest("StudentId or OfferId incorrect!");
            return Ok(new StudentDto().CopyPropertiesFrom(_service.AddStudentToWaitingList(studentId, offerId)));
        }

    }
}
