namespace LSWBackend.Services
{
    public class WaitingListsService
    {
        private readonly LSWContext _db;

        public WaitingListsService(LSWContext db) => _db = db;

        public WaitingList RemoveStudentFromWaitingList(int watchingListId)
        {
            try
            {
                var waitingList = _db.WaitingLists.Single(x => x.WaitingListId == watchingListId);
                _db.WaitingLists.Remove(waitingList);
                _db.SaveChanges();
                return waitingList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public WaitingList AddStudentToWaitingList(int studentId, int offerId)
        {
            try
            {
                //checked if ids are valid
                var student = _db.Students.Single(x => x.StudentId == studentId);
                var offer = _db.Offers.Single(x => x.OfferId == offerId);
            }
            catch (Exception ex)
            {
                return new WaitingList();
            }
            var waitingList = new WaitingList
            {
                OfferId = offerId,
                StudentId = studentId,
                WaitingSince = DateTime.Now
            };
            _db.WaitingLists.Add(waitingList);
            _db.SaveChanges();
            return waitingList;
        }

    }
}
