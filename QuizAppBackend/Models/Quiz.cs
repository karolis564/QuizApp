namespace QuizAppBackend.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string QuestionType { get; set; } 
        public List<string> Options { get; set; } = new List<string>();  

     
    }
}
