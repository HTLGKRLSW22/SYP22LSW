namespace LSWBackend.Services;

public class CoursesService
{
    private readonly LSWContext _db;

    public CoursesService(LSWContext db) => _db = db;

    public Offer EditCourse(int id, OfferPutDto offerPutDto) {
        try {
            var courseToEdit = _db.Offers.Single(x => x.OfferId == id);
            courseToEdit.CopyPropertiesFrom(offerPutDto);
            _db.SaveChanges();
            return courseToEdit;
        }
        catch (InvalidOperationException) {
            return new Offer { OfferId = -1 };
        }
        catch (DbUpdateException) {
            return new Offer { OfferId = -2 };
        }
    }

    public IEnumerable<OfferDto> GetTeacherCourses(int teacherId) {
        try {
            return _db.Offers
                .Include(x => x.OfferDates)
                .Include(x => x.Teacher)
                .Where(x => x.TeacherId == teacherId)
                .Select(x => new OfferDto {
                    OfferId = x.OfferId,
                    TeacherId = x.TeacherId,
                    Teacher = x.Teacher,
                    OfferDates = x.OfferDates,
                    Title = x.Title
                })
                .ToList();
        }
        catch (Exception ex) {
            return new List<OfferDto>();
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
