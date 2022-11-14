using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class Teacher
{
    public int TeacherId { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int IsAdmin { get; set; }

    public virtual List<Offer> Offers { get; set; }
    public virtual List<Report> Reports { get; set; }
    public virtual List<OfferTeacher> OfferTeachers { get; set; }
    public virtual List<TeacherLesson> TeachersLessons { get; set; }
}
