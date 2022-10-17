namespace LSWBackend.Services
{
    public class OfferService
    {
        private LSWContext db;

        public OfferService(LSWContext db)
        {
            this.db = db;
        }

        public bool DeleteOffer(out string errorMsg, int offerId)
        {
            if (db.Offers.Where(x => x.OfferId == offerId).Count() == 0)
            {
                errorMsg = $"No offer found with id: {offerId}";
                return false;
            }
            db.Offers.Remove(db.Offers.Single(x => x.OfferId == offerId));
            db.SaveChanges();
            errorMsg = "Success";
            return true;
        }

        public bool CreateOffer(out string errorMsg, int teacherId, Offer offer)
        {
            if (db.Teachers.Where(x => x.TeacherId == teacherId).Count() == 0)
            {
                errorMsg = $"No Teacher found with id: {teacherId}";
                return false;
            }
            db.Teachers.Single(x => x.TeacherId == teacherId).Offers.Add(offer);
            db.SaveChanges();
            errorMsg = "Success";
            return true;
        }


        public bool AddStudentToOffer(out string errorMsg, StudentOffer studentOffer)
        {
            var offer = db.Offers.Include(x => x.StudentOffers).SingleOrDefault(x => x.OfferId == studentOffer.OfferId);
            var student = db.Students.SingleOrDefault(x => x.StudentId == studentOffer.StudentId);
            if (offer == null || student == null)
            {
                errorMsg = offer == null ? $"No offer found with id: {studentOffer.OfferId}" : $"No student found with id: {studentOffer.StudentId}";
                return false;
            }
            offer.StudentOffers.Add(studentOffer);
            db.SaveChanges();
            errorMsg = "Success";
            return true;
        }

        public bool RemoveStudentFromOffer(out string errorMsg, StudentOffer studentOffer)
        {
            var offer = db.Offers.Include(x => x.StudentOffers).SingleOrDefault(x => x.OfferId == studentOffer.OfferId);
            var student = db.Students.SingleOrDefault(x => x.StudentId == studentOffer.StudentId);
            if (offer == null || student == null)
            {
                errorMsg = offer == null ? $"No offer found with id: {studentOffer.OfferId}" : $"No student found with id: {studentOffer.OfferId}";
                return false;
            }
            offer.StudentOffers.Remove(studentOffer);
            db.SaveChanges();
            errorMsg = "Success";
            return true;
        }

        public Offer EditOffer(int id, Offer offer)
        {
            try
            {
                var courseToEdit = db.Offers.Single(x => x.OfferId == id);
                courseToEdit.CopyPropertiesFrom(offer);
                db.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                return new Offer();
            }
            return null;
        }
    }
}
