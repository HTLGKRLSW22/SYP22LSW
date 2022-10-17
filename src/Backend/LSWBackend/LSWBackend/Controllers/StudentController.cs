using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private StudentService service;

        public StudentController(StudentService service)
        {
            this.service = service;
        }

        [HttpGet("Offers/{studentId}")]
        public ActionResult<List<OfferDto>> GetOffersOfStudent(int studentId)
        {
            string errorMsg;
            if (studentId <= 0 || studentId == default(int)) return BadRequest("courseId cannot be smaller than zero or default!");
            if (service.GetOffersOfStudents(out errorMsg, studentId)==null) return BadRequest(errorMsg);
            return Ok(service.GetOffersOfStudents(out errorMsg, studentId).Select(x => new OfferDto().CopyPropertiesFrom(x));
        }
    }
}
