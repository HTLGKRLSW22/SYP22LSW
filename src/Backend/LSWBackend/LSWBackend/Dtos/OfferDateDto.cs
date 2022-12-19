using System;
namespace LSWBackend.Dtos;

public class OfferDateDto
{
    [Required] public int OfferDateId { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
}

