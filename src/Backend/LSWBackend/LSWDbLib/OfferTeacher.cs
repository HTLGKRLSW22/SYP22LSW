using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class OfferTeacher
{
    public int OfferTeacherId { get; set; }
    public int TeacherId { get; set; }
    public int OfferId { get; set; }

    public Teacher Teacher { get; set; }
    public Offer Offer { get; set; }
}
