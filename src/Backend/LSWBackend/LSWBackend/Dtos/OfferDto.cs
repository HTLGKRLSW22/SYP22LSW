namespace LSWBackend.Dtos;

public class OfferDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public List<string> Teachers { get; set; } = null!;
    public double? Price { get; set; } = null!;
    public List<string> StartDates { get; set; } = null!;
    public List<string> EndDates { get; set; } = null!;
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public string? Location { get; set; }
    public string? MeetingPoint { get; set; }
    public bool? Enrolled { get; set; }
}
