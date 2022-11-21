using System.Security.Claims;

using LSWBackend.Dtos;

using LSWBackend.Dtos;
using LSWBackend.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentController : ControllerBase
{

    private readonly StudentService _service;

    public StudentController(StudentService service) => _service = service;

    [Authorize(Roles = "teacher")]
    [HttpGet]
    public ActionResult<StudentDto> GetStudentOffers() {
        int userId = int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        string errorMsg = "";
        try {
            var studentOffers = new StudentDto().CopyPropertiesFrom(_service.GetOffersOfStudents(out errorMsg, userId));
            return Ok(studentOffers);
        }
        catch {
            return BadRequest(errorMsg);
        }
    }

    //private OfferService _service;

    //public StudentController(OfferService service)
    //{
    //    this._service = service;
    //}


    //[HttpPost]
    //public ActionResult DeleteStudentFromCourse(int courseId, int studentId)
    //{
    //    /*_service.RemoveStudentFromCourse(new StudentCourseDto
    //    {
    //        CourseId = courseId,
    //        StudentId = studentId
    //    });*/
    //    return Ok();
    //}

    //[HttpDelete]
    //public ActionResult<StudentDto> AddStudentToCourse(int courseId, int studentId)
    //{
    //    /*
    //    var studentCourseDto = new StudentDto().CopyPropertiesFrom(_service.AddStudentToCourse(new StudentCourseDto
    //    {
    //        CourseId = courseId,
    //        StudentId = studentId
    //    }));*/
    //    return Ok();

    //}

    //[HttpGet("GetStudent")]
    //public ActionResult<StudentDto> GetStudent()
    //{
    //    return Ok(_service.GetStudent());
    //}
}
