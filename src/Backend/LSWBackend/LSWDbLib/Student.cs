namespace LSWDbLib;

public class Student
{
    public int StudentId { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int ClazzId { get; set; }

    public Clazz Clazz { get; set; } = null!;
    public virtual List<StudentOffer> StudentOffers { get; set; } = null!;
    public virtual List<WaitingList> WaitingLists { get; set; } = null!;
}
