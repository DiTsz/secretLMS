namespace reSmart.ViewModels
{
    public class CourseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Homework Description")]
        public string HomeworkDescription { get; set; }
    }
}
