using System;
namespace LSWBackend.Dtos
{
    public class OfferTeacherDTO
    {
        public OfferTeacherDTO()
        {
        }

        public int OfferTeacherId { get; set; }
        public int TeacherId { get; set; }
        public int OfferId { get; set; }
    }
}

