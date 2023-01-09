using System.Linq;

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

    public List<StudentWithOffersDto> GetClassOfTeacher(int teacherId) {
        Console.WriteLine(teacherId);
        return _db.Students.Include(x => x.Clazz).Include(x => x.StudentOffers).ThenInclude(x => x.Offer).ThenInclude(x => x.OfferDates).Where(x => x.Clazz.TeacherId == teacherId).Select(x => new StudentWithOffersDto() {
            ClassName = x.Clazz.ClazzName,
            FirstName = x.FirstName,
            LastName = x.LastName,
            StudentId = x.StudentId,
            SelectedOffers = x.StudentOffers.Select(j => new OfferOfStudentDto {
                Date = j.Offer.OfferDates[0].StartDate,
                OfferName = j.Offer.Title,
            }).ToList(),
            IsFreigestellt = x.StudentOffers.Count() == 0 ? false : x.StudentOffers[0].Offer.VisibleForStudents == 0
        }).ToList(); 

    }
}