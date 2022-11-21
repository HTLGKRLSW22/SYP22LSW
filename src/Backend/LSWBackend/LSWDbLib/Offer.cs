namespace LSWDbLib;

public class Offer
{
    public int OfferId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MinStudents { get; set; }
    public int MaxStudents { get; set; }
    public string MeetingPoint { get; set; } = null!;
    public decimal Costs { get; set; }
    public string Location { get; set; } = null!;
    public int VisibleForStudents { get; set; }
    public int? TeacherId { get; set; }

    public Teacher? Teacher { get; set; }
    public virtual List<OfferDate> OfferDates { get; set; } = null!;
    public virtual List<OfferTeacher> OfferTeachers { get; set; } = null!;
    public virtual List<StudentOffer> StudentOffers { get; set; } = null!;
    public virtual List<ClassOffer> ClassOffers { get; set; } = null!;
}
