namespace LSWDbLib;

public class ClassOffer
{
    public int ClassOfferId { get; set; }
    public int OfferId { get; set; }
    public int ClazzId { get; set; }

    public Offer Offer { get; set; } = null!;
    public Clazz Clazz { get; set; } = null!;
}
