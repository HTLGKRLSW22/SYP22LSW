namespace LSWBackend.Dtos;

public class StudentDto
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int CourseId { get; set; }
    public int ClassId { get; set; }
    public int? WaitinglistId { get; set; }
}
