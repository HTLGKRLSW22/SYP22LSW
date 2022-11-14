using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class Student
{
    public int StudentId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int ClazzId { get; set; }

    public Clazz Clazz { get; set; }
    public virtual List<StudentOffer> StudentOffers { get; set; }
    public virtual List<WaitingList> WaitingLists { get; set; }
}
