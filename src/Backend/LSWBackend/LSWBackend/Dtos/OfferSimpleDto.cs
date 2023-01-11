namespace LSWBackend.Dtos;

public class OfferSimpleDto
{
    public int OfferId { get; set; }
    public string OfferName { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public string TeacherName { get; set; } = null!;
}
