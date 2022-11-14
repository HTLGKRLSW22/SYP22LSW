using System;
namespace LSWBackend.Dtos
{
    public class OfferDateDTO
    {
        public OfferDateDTO()
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int OfferId { get; set; }
    }
}

