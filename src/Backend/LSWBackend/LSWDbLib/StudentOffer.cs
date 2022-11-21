namespace LSWDbLib;

public class StudentOffer
{
    public int StudentOfferId { get; set; }
    public int StudentId { get; set; }
    public int OfferId { get; set; }

    public Student Student { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
}
