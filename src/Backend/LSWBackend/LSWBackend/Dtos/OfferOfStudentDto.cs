namespace LSWBackend.Dtos;

public class OfferOfStudentDto
{
    [Required] public DateTime Date { get; set; }
    [Required] public string OfferName { get; set; } = null!;
}
