using System;
namespace LSWBackend.Dtos;

public class OfferDto
{
    public int OfferId { get; set; }
    public string Title { get; set; } = null!;
    public List<OfferDate> OfferDates { get; set; } = new();
    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
}

