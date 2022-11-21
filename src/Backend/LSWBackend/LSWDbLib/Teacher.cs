namespace LSWDbLib;

public class Teacher
{
    public int TeacherId { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int IsAdmin { get; set; }

    public virtual List<Offer> Offers { get; set; } = null!;
    public virtual List<Report> Reports { get; set; } = null!;
    public virtual List<OfferTeacher> OfferTeachers { get; set; } = null!;
    public virtual List<TeacherLesson> TeachersLessons { get; set; } = null!;
}
