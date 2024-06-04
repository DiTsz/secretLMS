namespace reSmart.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public AppUser Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
