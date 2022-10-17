using Backend.Dtos;
using ChinookPlaylists;

namespace LSWBackend.Services
{
    public class CoursesService
    {
        private readonly LSWContext _db;

        public CoursesService(LSWContext db) => _db = db;

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

        public Offer EditCourse(int id, OfferDto offerDto)
        {
            try
            {
                var courseToEdit = _db.Offers.Single(x => x.OfferId == id);
                courseToEdit.CopyPropertiesFrom(offerDto);
                _db.SaveChanges();
                return courseToEdit;
            }
            catch (InvalidOperationException ex)
            {
                return new Offer();
            }
        }

        public IEnumerable<Offer> GetTeacherCourses(int teacherId)
        {
            try
            {
                return _db.Offers.Where(x => x.TeacherId == teacherId);
            }
            catch (Exception ex)
            {
                return new List<Offer>();
            }
        }
    }
}
