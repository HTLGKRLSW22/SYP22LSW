namespace Backend.Dtos
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
		public string Initials { get; set; }
		public int CourseId { get; set; }
    }
}
