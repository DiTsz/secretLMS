using static System.Net.Mime.MediaTypeNames;

namespace reSmart.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HomeworkDescription { get; set; }
        public string TeacherId { get; set; }
        public AppUser Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}