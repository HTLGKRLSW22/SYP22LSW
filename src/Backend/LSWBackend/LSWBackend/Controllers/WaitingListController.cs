using Backend.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitingListController : ControllerBase
    {
        private readonly OfferService _service;

        public WaitingListController(OfferService service) => _service = service;

        [HttpPut]
        public ActionResult<StudentDto> RemoveStudent(int waitingListId, int studentId)
        {
            if (waitingListId < 0 || studentId < 0) return BadRequest("WaitingListId or StudentId incorrect!");
            return Ok(new StudentDto());//.CopyPropertiesFrom(_service.RemoveStudentFromWaitingList(waitingListId, studentId));
        }

        [HttpPost]
        public ActionResult<StudentDto> AddStudent(/*int waitingListId, [FromBody] StudentDto studentDto*/)
        {
            //if (waitingListId < 0 || studentDto) return BadRequest("WaitingListId or request body incorrect!");
            return Ok(new StudentDto()); /*.CopyPropertiesFrom(_service.AddStudentToWaitingList(waitingListId, studentDto));*/
        }
    }
}
