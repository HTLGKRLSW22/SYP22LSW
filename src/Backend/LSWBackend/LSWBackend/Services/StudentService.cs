namespace LSWBackend.Services
{
    public class StudentService
    {
        private LSWContext db;

        public StudentService(LSWContext db)
        {
            this.db = db;
        }

        public List<StudentOffer> GetOffersOfStudents(out string errorMsg, int studentId) {
            if (db.Students.Where(x => x.StudentId == studentId).Count() == 0) {
                errorMsg = $"No Student found with id: {studentId}";
                return null;
            }
            errorMsg = "Success";
            var student = db.Students.Single(x => x.StudentId == studentId);
            var studentOffer = db.StudentOffers.Where(x => x.StudentOfferId == student.StudentId).ToList();
            return studentOffer;
        }
    }
}
