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
            .Include(x => x.Offer)
            .ToList();

        return availableDates
            .Select(x => new OfferOfStudentDto {
                Date = x.Date,
                OfferName = offerDates.FirstOrDefault(y => y.StartDate.Date == x.Date.Date)?.Offer.Title ?? "-",
            })
            .ToList();
    }


    public List<OfferDto> GetNotFullOffers() {
        //get all classoffers
        var classOffers = _db.ClassOffers
            .Include(x => x.Offer)
            .ThenInclude(x => x.OfferDates)
            .Include(x => x.Offer)
            .ThenInclude(x => x.StudentOffers)
            .Select(x => new OfferDto {
                TeacherNames = x.Offer.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToArray(),
                StartDates = x.Offer.OfferDates.Select(y => y.StartDate).ToArray(),
                EndDates = x.Offer.OfferDates.Select(y => y.EndDate).ToArray(),
                CurrentCount = x.Offer.StudentOffers.Count
            }.CopyPropertiesFrom(x.Offer))
                    .ToList();

        //get all offers and remove all classOffers
        var offers = _db.Offers
            .Include(x => x.OfferDates)
            .Include(x => x.StudentOffers)
            .Select(x => new OfferDto {
                TeacherNames = x.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToArray(),
                StartDates = x.OfferDates.Select(y => y.StartDate).ToArray(),
                EndDates = x.OfferDates.Select(y => y.EndDate).ToArray(),
                CurrentCount = x.StudentOffers.Count
            }.CopyPropertiesFrom(x))
                    .ToList();
        var notFullOffers = offers.Where(x => !classOffers.Any(y => y.OfferId == x.OfferId)).ToList();

        //var offers = _db.Offers
        //    .Where(x => x.MaxStudents > x.StudentOffers.Count && x.OfferDates.Count == 1)
        //    .Include(x => x.OfferDates)
        //    .Include(x => x.StudentOffers)
        //    .Select(x => new OfferDto {
        //        TeacherNames = x.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToArray(),
        //        StartDates = x.OfferDates.Select(y => y.StartDate).ToArray(),
        //        EndDates = x.OfferDates.Select(y => y.EndDate).ToArray(),
        //        CurrentCount = x.StudentOffers.Count
        //    }.CopyPropertiesFrom(x))
        //    .ToList();

        return notFullOffers;
        
        
    }
}
