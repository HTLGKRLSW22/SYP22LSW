using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class Offer
{
    public int OfferId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MinStudents { get; set; }
    public int MaxStudents { get; set; }
    public string MeetingPoint { get; set; }
    public decimal Costs { get; set; }
    public string Location { get; set; }
    public int? TeacherId { get; set; }

    public Teacher? Teacher { get; set; }
    public virtual List<OfferDate> OfferDates { get; set; }
    public virtual List<OfferTeacher> OfferTeachers { get; set; }
    public virtual List<StudentOffer> StudentOffers { get; set; }
    public virtual List<ClassOffer> ClassOffers { get; set; }
}
