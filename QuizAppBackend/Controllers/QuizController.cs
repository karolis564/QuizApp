using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Models;
using System.Text.RegularExpressions;

namespace QuizAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        
        private readonly QuizDbContext _context;

        public QuizController(QuizDbContext context)
        {
         _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Quiz>>> GetQuizzes()
        {
         var quizzes = await _context.Quizzes.ToListAsync();
         return Ok(quizzes);
        }
[HttpPost("submit-answers")]
public async Task<IActionResult> SubmitAnswers([FromBody] AnswerSubmission answerSubmission)
{
    if (answerSubmission == null || !answerSubmission.Answers.Any())
    {
        return BadRequest("No answers provided.");
    }

    var emailSubmission = await _context.EmailSubmissions.FindAsync(answerSubmission.EmailSubmissionId);
    if (emailSubmission == null)
    {
        return NotFound("Email submission not found.");
    }

    int totalScore = 0;

    foreach (var answer in answerSubmission.Answers)
    {
        var quiz = await _context.Quizzes.FindAsync(answer.QuizId);
        if (quiz == null)
        {
            return BadRequest($"Invalid QuizId: {answer.QuizId}");
        }

        if (quiz.QuestionType == "radio")
        {
            if (answer.AnswerText.Equals(quiz.Answer, StringComparison.OrdinalIgnoreCase))
            {
                answer.PointCollected = 100;
                totalScore += 100;
            }
        }
else if (quiz.QuestionType == "checkbox")
{
    // Normalize and split the correct and user answers
    var correctAnswers = quiz.Answer.Split(',').Select(a => a.Trim().ToLower()).ToList();
    var userAnswers = answer.AnswerText.Split(',').Select(a => a.Trim().ToLower()).ToList();

    // Debug: Show correct and user answers
    Console.WriteLine("Correct Answers: " + string.Join(", ", correctAnswers));
    Console.WriteLine("User Answers: " + string.Join(", ", userAnswers));

    // Count how many answers the user selected correctly
    int correctCount = userAnswers.Count(userAnswer => correctAnswers.Contains(userAnswer));

    // Debug: Show correct count
    Console.WriteLine($"Correct Count: {correctCount} of {correctAnswers.Count}");

    // Total correct answers in the quiz
    int totalCorrectAnswers = correctAnswers.Count;

    // Points per correct answer (use double to keep decimals)
    double possiblePointsPerAnswer = 100.0 / totalCorrectAnswers;

    // Debug: Show possible points per answer
    Console.WriteLine($"Points per correct answer: {possiblePointsPerAnswer}");

    // Calculate the total points the user should get based on how many answers were correct
    double totalPoints = possiblePointsPerAnswer * correctCount;

    // Debug: Show the calculated total points
    Console.WriteLine($"Total Points (before rounding): {totalPoints}");

    // Round the total points to the nearest integer
    answer.PointCollected = (int)Math.Round(totalPoints);

    // Debug: Show the final points
    Console.WriteLine($"Points Collected: {answer.PointCollected}");

    // Add to total score
    totalScore += answer.PointCollected;

    // Debug: Show the running total score
    Console.WriteLine($"Running Total Score: {totalScore}");
}




        else if (quiz.QuestionType == "textbox")
        {
            if (answer.AnswerText.Equals(quiz.Answer, StringComparison.OrdinalIgnoreCase))
            {
                answer.PointCollected = 100;
                totalScore += 100;
            }
        }
    }

    _context.AnswerSubmissions.Add(answerSubmission);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Answers submitted successfully!", totalScore });
}



[HttpGet("get-answers/{emailSubmissionId}")]
public async Task<IActionResult> GetAnswers(int emailSubmissionId)
{
    var answerSubmission = await _context.AnswerSubmissions
        .Include(a => a.Answers)
        .ThenInclude(ans => ans.Quiz) // Optional: Include Quiz details if needed
        .FirstOrDefaultAsync(sub => sub.EmailSubmissionId == emailSubmissionId);

    if (answerSubmission == null)
    {
        return NotFound("No answers found for the given EmailSubmissionId.");
    }

    return Ok(answerSubmission);
}

[HttpGet("top-scores")]
public async Task<ActionResult<List<object>>> GetTopScores()
{
    // Get the top scores by grouping by Email
    var topScores = await _context.AnswerSubmissions
        .Include(a => a.Answers) // Include Answers in the query
        .Join(_context.EmailSubmissions, 
              answerSubmission => answerSubmission.EmailSubmissionId, 
              emailSubmission => emailSubmission.Id, 
              (answerSubmission, emailSubmission) => new 
              {
                  emailSubmission.Email,
                  TotalScore = answerSubmission.Answers.Sum(a => a.PointCollected)
              })
        .GroupBy(x => x.Email)
        .Select(g => new 
        {
            Email = g.Key,
            TotalScore = g.Sum(x => x.TotalScore)
        })
        .OrderByDescending(x => x.TotalScore)
        .Take(10)
        .ToListAsync();

    // Return the top scores
    return Ok(topScores);
}




            [HttpPost("submit-email")]
            public async Task<IActionResult> SubmitEmail([FromBody] EmailSubmission submission)
            {
                if (submission == null || string.IsNullOrEmpty(submission.Email))
                {
                 return BadRequest("Email is required.");
                }
            
                _context.EmailSubmissions.Add(submission);
                await _context.SaveChangesAsync();
    
                return Ok(new { message = "Email submission received.", id = submission.Id });
            }

            [HttpGet("emails")]
            public async Task<ActionResult<List<EmailSubmission>>> GetEmailSubmissions()
              {
                var emails = await _context.EmailSubmissions.ToListAsync();
                return Ok(emails);
              }
            private bool IsValidEmail(string email)
              {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
              }     
     } 
}
