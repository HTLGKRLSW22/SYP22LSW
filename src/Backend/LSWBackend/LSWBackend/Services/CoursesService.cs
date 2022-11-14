using Backend.Dtos;

using ChinookPlaylists;

namespace LSWBackend.Services;

public class CoursesService
{
    private readonly LSWContext _db;

    public CoursesService(LSWContext db) => _db = db;

    public Offer EditCourse(int id, OfferDto offerDto) {
        try {
            var courseToEdit = _db.Offers.Single(x => x.OfferId == id);
            courseToEdit.CopyPropertiesFrom(offerDto);
            _db.SaveChanges();
            return courseToEdit;
        }
        catch (InvalidOperationException) {
            return new Offer();
        }
    }

    public IEnumerable<Offer> GetTeacherCourses(int teacherId) {
        try {
            return _db.Offers.Where(x => x.TeacherId == teacherId);
        }
        catch (Exception ex) {
            return new List<Offer>();
        }
    }
}
