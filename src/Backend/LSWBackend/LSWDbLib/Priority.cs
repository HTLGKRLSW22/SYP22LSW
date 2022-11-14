using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class Priority
{
    public int PriorityId { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }

    [ForeignKey("FirstPriority")]
    public int? FirstPriorityId { get; set; }
    public Offer? FirstPriority { get; set; }

    [ForeignKey("SecondPriority")]
    public int? SecondPriorityId { get; set; }
    public Offer? SecondPriority { get; set; }

    [ForeignKey("ThirdPriority")]
    public int? ThirdPriorityId { get; set; }
    public Offer? ThirdPriority { get; set; }
}
