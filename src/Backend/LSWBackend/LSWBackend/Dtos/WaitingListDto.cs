namespace Backend.Dtos;

public class WaitingListDto
{
    public int WaitingListId { get; set; }
    public DateTime WaitingSince { get; set; }
    public int StudentId { get; set; }
    public int OfferId { get; set; }
}
