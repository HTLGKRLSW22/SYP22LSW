namespace LSWBackend.Dtos;

public class CreateOfferDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public double? Price { get; set; }
    public List<string> StartDates { get; set; } = null!;
    public List<string> EndDates { get; set; } = null!;
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public string? Location { get; set; }
    public string? MeetingPoint { get; set; }
    public List<string> Clazzes { get; set; } = null!;
}
