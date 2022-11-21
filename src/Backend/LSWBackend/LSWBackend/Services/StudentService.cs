namespace LSWBackend.Services;

public class StudentService
{
    private readonly LSWContext _db;

    public StudentService(LSWContext db) => _db = db;

    public List<StudentOffer>? GetOffersOfStudents(out string errorMsg, int studentId) {
        if (_db.Students.Where(x => x.StudentId == studentId).Any()) {
            errorMsg = $"No Student found with id: {studentId}";
            return null;
        }
        errorMsg = "Success";
        var student = _db.Students.Single(x => x.StudentId == studentId);
        var studentOffer = _db.StudentOffers.Where(x => x.StudentOfferId == student.StudentId).ToList();
        return studentOffer;
    }
}
