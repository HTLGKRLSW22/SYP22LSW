namespace LSWBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly StudentsService _studentsService;

    public StudentsController(StudentsService studentsService) => _studentsService = studentsService;


    [HttpGet("StudentsWithOffers")]
    public List<StudentWithOffersDto> GetStudentsWithOffers() {
        return _studentsService.GetStudentsWithOffers();
    }

    [HttpGet("GetClassOfTeacher")]
    public List<StudentWithOffersDto> GetClassOfTeacher(int teacherId) {
        return _studentsService.GetClassOfTeacher(teacherId);
    }

}
