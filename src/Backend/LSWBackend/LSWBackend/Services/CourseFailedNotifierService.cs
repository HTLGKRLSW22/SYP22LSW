namespace LSWBackend.Services;

public class CourseFailedNotifierService
{
    private readonly LSWContext _db;
    private readonly SendEmailsService _emailService;

    public CourseFailedNotifierService(LSWContext db, SendEmailsService emailService) {
        _db = db;
        _emailService = emailService;
    }

    public void NotifyStudentsCourseFailed() {
        var studentsWithCourses = StudentsWithCoursesFailed();

        foreach (var keyValue in studentsWithCourses) {
            foreach (string course in keyValue.Value) {
                _emailService.SendNotificationCourseFailed($"{keyValue.Key.Username}@sus.htl-grieskirchen.at", course);
            }
        }
    }

    private Dictionary<Student, List<string>> StudentsWithCoursesFailed() {
        var output = new Dictionary<Student, List<string>>();

        var failedOffers = _db.Offers
            .Include(x => x.StudentOffers)
            .ThenInclude(x => x.Student)
            .Where(x => x.StudentOffers.Count < x.MinStudents)
            .ToList();

        foreach (var offer in failedOffers) {
            string courseName = offer.Title;

            offer.StudentOffers
                .Select(x => x.Student)
                .ToList()
                .ForEach(x => {
                    if (output.ContainsKey(x)) {
                        output[x].Add(courseName);
                    }
                    else {
                        var list = new List<string> {
                            courseName
                        };

                        output.Add(x, list);
                    }
                });

            offer.StudentOffers.Clear();
            _db.SaveChanges();
        }

        return output;
    }
}
