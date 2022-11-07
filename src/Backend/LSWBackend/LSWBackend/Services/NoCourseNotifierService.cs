namespace LSWBackend.Services
{
    public class NoCourseNotifierService
    {
        private readonly LSWContext _db;
        private readonly EmailSenderService _emailSender;

        private DateTime enrollmentDeadline = DateTime.Now;
        private int nofifyDaysBefore = 1;

        public NoCourseNotifierService(LSWContext db, EmailSenderService emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        private List<Student> GetStudentsWithoutEnrollment()
        {
            var dates = _db.OfferDates;

            return _db.Students
                 .Include(x => x.StudentOffers)
                 .ThenInclude(x => x.Offer)
                 .ThenInclude(x => x.OfferDates)
                 .Where(x => x.StudentOffers.Count == 0 || !IsStudentEnrolledOnAllDays(x))
                 .ToList();
        }

        private bool IsStudentEnrolledOnAllDays(Student student)
        {
            return student.StudentOffers.SelectMany(x => x.Offer.OfferDates).ToList().Count() < 3;
        }

        public void NotifyStudentsWithoutEnrollment()
        {
            var students = GetStudentsWithoutEnrollment();
            students.ForEach(x => Console.WriteLine($"{x.LastName}"));
        }
    }
}
