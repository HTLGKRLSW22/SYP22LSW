using System;
namespace LSWBackend.Dtos;

public class TeacherDto
{
    public TeacherDto() {
    }

    [Required] public int TeacherId { get; set; }
    [Required] public string Username { get; set; } = null!;
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public int IsAdmin { get; set; }
}

