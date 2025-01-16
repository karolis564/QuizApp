using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Models;
namespace QuizAppBackend.Models
{
  public class QuizDbContext : DbContext
{
    public QuizDbContext(DbContextOptions<QuizDbContext> options)
        : base(options)
    { }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<EmailSubmission> EmailSubmissions { get; set; }
    public DbSet<AnswerSubmission> AnswerSubmissions { get; set; }
    public DbSet<Answer> Answers { get; set; }


}}
