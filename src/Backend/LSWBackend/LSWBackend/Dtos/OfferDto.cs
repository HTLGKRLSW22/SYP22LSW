namespace LSWBackend.Dtos;

public class OfferDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public decimal? Costs { get; set; }
    public int MinStudents { get; set; }
    public int MaxStudents { get; set; }
    public string? Location { get; set; } = null!;
    public string? MeetingPoint { get; set; } = null!;

    // join other tables
    public string[] TeacherNames { get; set; } = null!;
    public DateTime[] StartDates { get; set; } = null!;
    public DateTime[] EndDates { get; set; } = null!;
    public int CurrentCount { get; set; }

    public bool? Registered { get; set; }
}
