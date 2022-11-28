namespace LSWBackend.Services;

public class NoCourseNotifierService
{
    private readonly LSWContext _db;
    private readonly SendEmailsService _sendEmailsService;

    public NoCourseNotifierService(LSWContext db, SendEmailsService _emailService) {
        _db = db;
        _sendEmailsService = _emailService;
    }

    public void NotifiyAllStudentsWithoutEnrollment(string endDate) {
        var students = GetStudentsWithoutEnrollment();

        students.ToList().ForEach(x => {
            _sendEmailsService.SendWarningTimeEndingSoon($"{x.Key.Username}@sus.htl-grieskirchen.at", endDate, x.Value.Select(x => x.ToString("dd.MM.yyyy")).ToArray());
        });
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

            if (missingDates.Count() == 0) {
                studentsWithoutEnrollment.Remove(x.Key);
            }
            else if (missingDates.Count() > 0) {
                studentsWithoutEnrollment[x.Key] = missingDates;
            }
        });

        return studentsWithoutEnrollment;
    }
}
