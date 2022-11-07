namespace LSWDbLib;

public class OfferTeacher
{
    public int OfferTeacherId { get; set; }
    public int TeacherId { get; set; }
    public int OfferId { get; set; }

    public Teacher Teacher { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
}
