namespace LSWBackend.Dtos;

public class CreateOfferDto
{
    public string Titel { get; set; } = null!;
    public string? Beschreibung { get; set; }
    public double? Preis { get; set; }
    public List<string> Startdatum { get; set; } = null!;
    public List<string> Enddatum { get; set; } = null!;
    public int MaxCount { get; set; }
    public int MinCount { get; set; }
    public string? Location { get; set; }
    public string? MeetingPoint { get; set; }
    public List<string> Klassen { get; set; } = null!;
}
