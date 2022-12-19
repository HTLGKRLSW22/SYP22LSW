namespace LSWBackend.Dtos;

public class OfferDto
{
    public string Titel { get; set; } = null!;
    public string? Beschreibung { get; set; }
    public List<string> Lehrer { get; set; } = null!;
    public double? Preis { get; set; } = null!;
    public List<string> Startdatum { get; set; } = null!;
    public List<string> Enddatum { get; set; } = null!;
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public string? Location { get; set; }
    public string? MeetingPoint { get; set; }
    public bool? Eingetragen { get; set; }
}
