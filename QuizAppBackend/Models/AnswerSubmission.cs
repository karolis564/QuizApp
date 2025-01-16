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
        public int Id { get; set; } // This will be the primary key
        public string AnswerText { get; set; }
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; } // Relationship with Quiz entity
        public int PointCollected { get; set; } // Points awarded for this answer
    } // Closing bracket for Answer class

  
}

