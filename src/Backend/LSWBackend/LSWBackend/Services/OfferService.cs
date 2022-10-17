namespace LSWBackend.Services
{
    public class OfferService
    {
        public OfferService(/*DbContext db*/)
        {
            //this.db = db;
        }

        public bool DeleteCourse(out string errorMsg, int courseId)
        {
            //if (db.Courses.Where(x => x.CourseId == courseId).Count() == 0)
            //{
            //  errorMsg = $"No Course found with id: {courseId}";
            //  return false;    
            //}
            //db.Courses.Remove(db.Courses.Single(x => x.CourseId == courseId));
            //db.SaveChanges();
            errorMsg = "Success";
            return true;
        }

        public bool CreateCourse(out string errorMsg, int teacherId/*, Course course*/)
        {
            //if (db.Teachers.Where(x => x.TeacherId == teacherId).Count() == 0)
            //{
            //  errorMsg = $"No Teacher found with id: {teacherId}";
            //  return false;    
            //}
            //db.Teachers.Single(x => x.TeacherId == teacherId).Courses.Add(course);
            //db.SaveChanges();
            errorMsg = "Success";
            return true;
        }


        public bool AddStudentToCourse(out string errorMsg/*, StudentCourseDto studentCourseDto*/)
        {
            /*var course = _db.Courses.Include(x => x.Students).SingleOrDefault(x => x.CourseId == studentCourseDto.CourseId);
            var student = _db.Students.SingleOrDefault(x => x.StudentId == studentCourseDto.StudentId);
            if(course == null || student == null)
            {
                errorMsg = course == null ? $"No course found with id: {studentCourseDto.CourseId}": $"No student found with id: {studentCourseDto.StudentId}";
                return false;
            }
            course.Students.Add(student);
            _db.SaveChanges();*/
            errorMsg = "Success";
            return true;
        }

        public bool RemoveStudentFromCourse(out string errorMsg/*, StudentCourseDto studentCourseDto*/)
        {
            /*var course = _db.Courses.Include(x => x.Students).SingleOrDefault(x => x.CourseId == studentCourseDto.CourseId);
            var student = _db.Students.SingleOrDefault(x => x.StudentId == studentCourseDto.StudentId);
            if (course == null || student == null)
            {
                errorMsg = course == null ? $"No course found with id: {studentCourseDto.CourseId}" : $"No student found with id: {studentCourseDto.StudentId}";
                return false;
            }
            course.Students.Remove(student);
            _db.SaveChanges();*/
            errorMsg = "Success";
            return true;
        }

        public void /*Course*/ EditCourse( /*int id, Course course*/)
        {
            try
            {
                //var courseToEdit = db.Courses.Single(x => x.CourseId == id);
                //courseToEdit.CopyPropertiesFrom(course);
                //db.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                //return new Course();
            }
        }


        #region WaitingList

        public void /*Student*/ RemoveStudentFromWaitingList(/*int waitingListId, int studentId*/)
        {
            //var student = db.Students.Single(x => x.StudentId == studentId);
            //db.WaitingLists.Single(x => x.WaitingListId == waitingListId).Students.Remove(student);
            //db.SaveChanges();
            //return student;
        }

        public void /*Student*/ AddStudentToWaitingList(/*int waitingListId, Student student*/)
        {
            //db.WaitingLists.Single(x => x.WaitingListId == waitingListId).Students.Add(student);
            //db.SaveChanges();
            //return student;
        }


        #endregion
    }
}
