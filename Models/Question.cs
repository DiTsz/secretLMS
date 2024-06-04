namespace reSmart.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
