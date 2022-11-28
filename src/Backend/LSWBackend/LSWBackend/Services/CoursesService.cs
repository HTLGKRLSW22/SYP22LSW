namespace LSWBackend.Services;

public class CoursesService
{
    private readonly LSWContext _db;

    public CoursesService(LSWContext db) => _db = db;

    public OfferDetailDto EditCourse(int id, OfferPutDto offerPutDto) {
        try {
            var courseToEdit = _db.Offers
                .Include(x => x.StudentOffers)
                .Include(x => x.OfferDates)
                .Include(x => x.OfferTeachers)
                .ThenInclude(x => x.Teacher)
                .Single(x => x.OfferId == id);
            courseToEdit.CopyPropertiesFrom(offerPutDto);
            _db.SaveChanges();
            return new OfferDetailDto() {
                TeacherNames = courseToEdit.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToArray(),
                StartDates = courseToEdit.OfferDates.Select(y => y.StartDate).ToArray(),
                EndDates = courseToEdit.OfferDates.Select(y => y.EndDate).ToArray(),
                CurrentCount = courseToEdit.StudentOffers.Count
            }.CopyPropertiesFrom(courseToEdit);
        }
        catch (InvalidOperationException) {
            return new OfferDetailDto { OfferId = -1 };
        }
        catch (DbUpdateException) {
            return new OfferDetailDto { OfferId = -2 };
        }
    }

    public IEnumerable<OfferDetailDto> GetTeacherCourses(int teacherId) {
        try {
            return _db.Offers
                .Include(x => x.StudentOffers)
                .Include(x => x.OfferDates)
                .Include(x => x.OfferTeachers)
                .ThenInclude(x => x.Teacher)
                .Where(x => x.TeacherId == teacherId)
                .Select(x => new OfferDetailDto {
                    TeacherNames = x.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToArray(),
                    StartDates = x.OfferDates.Select(y => y.StartDate).ToArray(),
                    EndDates = x.OfferDates.Select(y => y.EndDate).ToArray(),
                    CurrentCount = x.StudentOffers.Count
                }.CopyPropertiesFrom(x))
                .ToList();
        }
        catch (Exception ex) {
            return new List<OfferDetailDto>();
        }
    }

    // helper for indicating if teacher who wants to edit the specified course is actually the owner
    public bool IsTeachersCourse(int courseId, int teacherId) {
        try {
            var course = _db.Offers.Single(x => x.OfferId == courseId);
            return course.TeacherId == teacherId;
        }
        catch (Exception) {
            return false;
        }
    }

}
