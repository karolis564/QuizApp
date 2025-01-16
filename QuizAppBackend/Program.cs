using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseInMemoryDatabase("QuizDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<QuizDbContext>();

    // Populate Quizzes
    context.Quizzes.AddRange(
        new Quiz
        {
            Question = "What is the capital of Lithuania?",
            Answer = "Vilnius",
            QuestionType = "radio",
            Options = new List<string> { "Vilnius", "Kaunas", "Klaipėda", "Šiauliai" }
        },
        new Quiz
        {
            Question = "Select all the planets in the solar system.",
            Answer = "Mercury, Venus, Mars, Jupiter, Saturn, Uranus, Neptune",
            QuestionType = "checkbox",
            Options = new List<string> { "Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" }
        },
        new Quiz
        {
            Question = "Who developed the theory of relativity?",
            Answer = "Albert Einstein",
            QuestionType = "textbox"
        },
        new Quiz
        {
            Question = "Which languages are official in Switzerland?",
            Answer = "German, French, Italian, Romansh",
            QuestionType = "checkbox",
            Options = new List<string> { "German", "French", "Italian", "Romansh", "English" }
        },
        new Quiz
        {
            Question = "What is the square root of 64?",
            Answer = "8",
            QuestionType = "radio",
            Options = new List<string> { "6", "7", "8", "9" }
        },
        new Quiz
        {
            Question = "Name a programming language that is primarily used for web development.",
            Answer = "JavaScript",
            QuestionType = "textbox"
        },
        new Quiz
        {
            Question = "Which element has the chemical symbol 'O'?",
            Answer = "Oxygen",
            QuestionType = "radio",
            Options = new List<string> { "Oxygen", "Osmium", "Oganesson", "Oxide" }
        },
        new Quiz
        {
            Question = "Select all prime numbers below 10.",
            Answer = "2, 3, 5, 7",
            QuestionType = "checkbox",
            Options = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" }
        },
        new Quiz
        {
            Question = "Who wrote 'To Kill a Mockingbird'?",
            Answer = "Harper Lee",
            QuestionType = "textbox"
        },
        new Quiz
        {
            Question = "Which planet is known as the Red Planet?",
            Answer = "Mars",
            QuestionType = "radio",
            Options = new List<string> { "Mars", "Venus", "Jupiter", "Saturn" }
        }
    );

    // Populate EmailSubmissions
    context.EmailSubmissions.AddRange(
        new EmailSubmission { Email = "jonas@example.com" },
        new EmailSubmission { Email = "lukas@example.com" },
        new EmailSubmission { Email = "karolis@example.com" },
        new EmailSubmission { Email = "as@example.com" },
        new EmailSubmission { Email = "martynas@example.com" },
         new EmailSubmission { Email = "564@example.com" }
    );

    // Populate AnswerSubmissions
    context.AnswerSubmissions.AddRange(
        new AnswerSubmission
        {
            EmailSubmissionId = 1,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = "Vilnius", QuizId = 1, PointCollected = 100 },
                new Answer { AnswerText = "Mars", QuizId = 2, PointCollected = 80 }
            }
        },
        new AnswerSubmission
        {
            EmailSubmissionId = 2,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = "Kaunas", QuizId = 1, PointCollected = 200 },
                new Answer { AnswerText = "Venus", QuizId = 2, PointCollected = 500 }
            }
        },
        new AnswerSubmission
        {
            EmailSubmissionId = 3,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = "Kaunas", QuizId = 1, PointCollected = 5 },
                new Answer { AnswerText = "Venus", QuizId = 2, PointCollected = 7 }
            }
        },
        new AnswerSubmission
        {
            EmailSubmissionId = 4,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = "Kaunas", QuizId = 1, PointCollected = 100 },
                new Answer { AnswerText = "Venus", QuizId = 2, PointCollected = 7 }
            }
       },
        new AnswerSubmission
        {
            EmailSubmissionId = 5,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = "Kaunas", QuizId = 1, PointCollected = 1000 },
                new Answer { AnswerText = "Venus", QuizId = 2, PointCollected = 0 }
            }
         },
        new AnswerSubmission
        {
            EmailSubmissionId = 6,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = "Kaunas", QuizId = 1, PointCollected = 20 },
                new Answer { AnswerText = "Venus", QuizId = 2, PointCollected = 100 }
            }
        }
    );
    context.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
