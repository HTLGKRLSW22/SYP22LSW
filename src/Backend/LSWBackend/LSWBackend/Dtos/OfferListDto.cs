using System;
namespace LSWBackend.Dtos;

public class OfferListDto
{
    [Required] public int OfferId { get; set; }
    [Required] public string Title { get; set; } = null!;
    [Required] public List<OfferDate> OfferDates { get; set; } = new();
    [Required] public int? TeacherId { get; set; }
    [Required] public TeacherDto? Teacher { get; set; }
}

