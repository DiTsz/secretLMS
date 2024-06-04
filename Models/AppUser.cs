namespace reSmart.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> Courses { get; set; } // For teachers
    }
}
