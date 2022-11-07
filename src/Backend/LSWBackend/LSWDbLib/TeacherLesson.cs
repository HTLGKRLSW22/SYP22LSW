namespace LSWDbLib;

public class TeacherLesson
{
    public int TeacherLessonId { get; set; }
    public int TeacherId { get; set; }
    public DateTime LessonDate { get; set; }
    public int LessonAmount { get; set; }

    public Teacher Teacher { get; set; } = null!;
}
