namespace LSWBackend.Dtos;

public class StudentWithOffersDto
{
    [Required] public int StudentId { get; set; }
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public string ClassName { get; set; } = null!;
    [Required] public List<OfferOfStudentDto> SelectedOffers { get; set; } = new();
}
