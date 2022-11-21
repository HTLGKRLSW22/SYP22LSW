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
}
