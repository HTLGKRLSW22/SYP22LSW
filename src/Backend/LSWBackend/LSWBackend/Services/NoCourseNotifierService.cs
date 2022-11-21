namespace LSWBackend.Services;

public class NoCourseNotifierService
{
    private readonly LSWContext _db;

    public NoCourseNotifierService(LSWContext db) => _db = db;


    private List<Student> GetStudentsWithoutEnrollment() {
        var dates = _db.OfferDates;

        return _db.Students
             .Include(x => x.StudentOffers)
             .ThenInclude(x => x.Offer)
             .ThenInclude(x => x.OfferDates)
             .Where(x => x.StudentOffers.Count == 0 || !IsStudentEnrolledOnAllDays(x))
             .ToList();
    }

    private static bool IsStudentEnrolledOnAllDays(Student student) {
        return student.StudentOffers.SelectMany(x => x.Offer.OfferDates).ToList().Count < 3;
    }

    public void NotifyStudentsWithoutEnrollment() {
        var students = GetStudentsWithoutEnrollment();
        students.ForEach(x => Console.WriteLine($"{x.LastName}"));
    }
}
