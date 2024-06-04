namespace reSmart.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string StudentId { get; set; }
        public AppUser Student { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
    }
}
