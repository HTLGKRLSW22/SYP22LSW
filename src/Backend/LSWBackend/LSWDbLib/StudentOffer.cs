using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class StudentOffer
{
    public int StudentOfferId { get; set; }
    public int StudentId { get; set; }
    public int OfferId { get; set; }

    public Student Student { get; set; }
    public Offer Offer { get; set; }
}
