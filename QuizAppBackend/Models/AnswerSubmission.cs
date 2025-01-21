namespace QuizAppBackend.Models
{
  public class AnswerSubmission
{
    public int Id { get; set; }
    public int EmailSubmissionId { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
   
}
    public class Answer
    {
        public int Id { get; set; } 
        public string AnswerText { get; set; }
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; } 
        public int PointCollected { get; set; } 
    } 

  
}

