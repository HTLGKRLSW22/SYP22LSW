using Backend.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private OfferService _service;

        public StudentController(OfferService service)
        {
            this._service = service;
        }


        [HttpPost]
        public ActionResult DeleteStudentFromCourse(int courseId, int studentId)
        {
            /*_service.RemoveStudentFromCourse(new StudentCourseDto
            {
                CourseId = courseId,
                StudentId = studentId
            });*/
            return Ok();
        }

        [HttpDelete]
        public ActionResult<StudentDto> AddStudentToCourse(int courseId, int studentId)
        {
            /*
            var studentCourseDto = new StudentDto().CopyPropertiesFrom(_service.AddStudentToCourse(new StudentCourseDto
            {
                CourseId = courseId,
                StudentId = studentId
            }));*/
            return Ok();

        }

        [HttpGet("GetStudent")]
        public ActionResult<StudentDto> GetStudent()
        {
            return Ok(_service.GetStudent());
        }
    }
}
