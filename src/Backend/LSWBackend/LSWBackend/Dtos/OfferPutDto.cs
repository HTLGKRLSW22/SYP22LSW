namespace LSWBackend.Dtos;

public class OfferPutDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MinStudents { get; set; }
    public int MaxStudents { get; set; }
    public string MeetingPoint { get; set; } = null!;
    public decimal Costs { get; set; }
    public string Location { get; set; } = null!;
    public int VisibleForStudents { get; set; }
    public int? TeacherId { get; set; }
}
