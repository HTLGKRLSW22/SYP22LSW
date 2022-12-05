namespace LSWDbLib;

public class OfferDate
{
    public int OfferDateId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int OfferId { get; set; }

    public Offer Offer { get; set; } = null!;
}
