using System;
namespace LSWBackend.Dtos;

public class OfferTeacherDto
{
    public OfferTeacherDto() {
    }

    [Required] public int OfferTeacherId { get; set; }
    [Required] public int TeacherId { get; set; }
    [Required] public int OfferId { get; set; }
}

