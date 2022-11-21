namespace LSWDbLib;

public class WaitingList
{
    public int WaitingListId { get; set; }
    public DateTime WaitingSince { get; set; }
    public int StudentId { get; set; }
    public int OfferId { get; set; }

    public Student Student { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
}
