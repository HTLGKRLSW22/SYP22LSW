using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class ClassOffer
{
    public int ClassOfferId { get; set; }
    public int OfferId { get; set; }
    public int ClazzId { get; set; }

    public Offer Offer { get; set; }
    public Clazz Clazz { get; set; }
}
