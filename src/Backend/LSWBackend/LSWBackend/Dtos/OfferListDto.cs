using System;
namespace LSWBackend.Dtos;

public class OfferListDto
{
    [Required] public int OfferId { get; set; }
    [Required] public string Title { get; set; } = null!;
    [Required] public List<OfferDateDto> OfferDates { get; set; } = new();
    [Required] public string TeacherFirstname { get; set; } = null!;
    [Required] public string TeacherLastname { get; set; } = null!;
}

