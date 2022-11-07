namespace LSWBackend.Services
{
    public class StudentService
    {
        private LSWContext db;

        public StudentService(LSWContext db)
        {
            this.db = db;
        }

        public List<Offer> GetOffersOfStudents(out string errorMsg, int studentId)
        {
            if (db.Students.Where(x => x.StudentId == studentId).Count() == 0)
            {
                errorMsg = $"No Student found with id: {studentId}";
                return null;
            }
            errorMsg = "Success";
            return db.Students.Single(x => x.StudentId == studentId)
                .StudentOffers.Select(x => db.Offers.Where(y => y.OfferId == x.OfferId)).ToList();
        }
    }
}
