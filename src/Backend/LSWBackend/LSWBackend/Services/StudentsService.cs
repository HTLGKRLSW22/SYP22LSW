namespace LSWBackend.Services;

public class StudentsService
{
    private readonly LSWContext _db;

    public StudentsService(LSWContext db) => _db = db;


    public List<StudentWithOffersDto> GetStudentsWithOffers() {
        return _db.Students
            .Include(x => x.Clazz)
            .AsEnumerable()
            .Select(x => new StudentWithOffersDto {
                StudentId = x.StudentId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ClassName = x.Clazz.ClazzName,
                SelectedOffers = GetOffersOfStudent(x.StudentId),
            })
            .ToList();
    }


    private List<OfferOfStudentDto> GetOffersOfStudent(int studentId) {
        var availableDates = _db.AvailableDates.ToList();

        var offerDates = _db.StudentOffers
            .Where(x => x.StudentId == studentId)
            .SelectMany(x => x.Offer.OfferDates)
            .ToList();

        return availableDates
            .Select(x => new OfferOfStudentDto {
                Date = x.Date,
                OfferName = offerDates.FirstOrDefault(y => y.StartDate.Date == x.Date.Date)?.Offer.Title ?? "-",
            })
            .ToList();
    }
}
