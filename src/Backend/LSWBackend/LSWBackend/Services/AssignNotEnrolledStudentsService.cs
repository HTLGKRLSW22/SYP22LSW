namespace LSWBackend.Services;

public class AssignNotEnrolledStudentsService
{
    private readonly LSWContext _db;
    private readonly SendEmailsService _emailService;

    public AssignNotEnrolledStudentsService(LSWContext db, SendEmailsService emailService) {
        _db = db;
        _emailService = emailService;
    }

    public void NotifyStudentsAssigned() {

    }

    private Dictionary<Student, string> AssignStudents() {
        Dictionary<Student, string> output = new Dictionary<Student, string>();
        List<Offer> offers = _db.Offers.OrderBy(x => x.StudentOffers.Count).ToList();

        foreach(var student in GetStudentsWithoutEnrollment()) {
            var offer = FindFirstSuitableOffer(offers, student.Value);
        }

        return output;
    }

    private Offer FindFirstSuitableOffer(List<Offer> offers, List<DateTime> dates) {
        throw new NotImplementedException();
    }

    private Dictionary<Student, List<DateTime>> GetStudentsWithoutEnrollment() {
        var requiredDates = _db.AvailableDates.Select(x => x.Date.Date).ToList();

        var studentsWithoutEnrollment = _db.Students.ToDictionary(x => x, x => requiredDates);

        var studentOfferDates = new Dictionary<Student, List<OfferDate>>();

        _db.StudentOffers
             .Include(x => x.Offer)
             .ThenInclude(x => x.OfferDates)
             .Include(x => x.Student)
             .ToList()
             .ForEach(x => {
                 if (studentOfferDates.ContainsKey(x.Student)) {
                     studentOfferDates[x.Student].AddRange(x.Offer.OfferDates);
                 }
                 else {
                     studentOfferDates.Add(x.Student, x.Offer.OfferDates);
                 }
             });

        studentOfferDates.ToList().ForEach(x => {
            var offerDates = x.Value.Select(x => x.StartDate.Date).Distinct();
            var missingDates = requiredDates.Except(offerDates).ToList();

            if (missingDates.Count == 0) {
                studentsWithoutEnrollment.Remove(x.Key);
            }
            else if (missingDates.Count > 0) {
                studentsWithoutEnrollment[x.Key] = missingDates;
            }
        });

        return studentsWithoutEnrollment;
    }
}
