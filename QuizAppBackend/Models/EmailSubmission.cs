namespace QuizAppBackend.Models
{
   public class EmailSubmission
{
    public int Id { get; set; }
    public string Email { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    
}
}
