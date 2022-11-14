using System;
namespace LSWBackend.Dtos;

public class OfferDto
{
    public int OfferId { get; set; }
    public string Title { get; set; } = null!;
    public List<OfferDateDTO> OfferDates { get; set; } = new();
    public List<OfferTeacherDTO> OfferTeachers { get; set; } = new();
}

