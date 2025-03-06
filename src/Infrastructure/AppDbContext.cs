using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Survey> Surveys => Set<Survey>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Result> Results => Set<Result>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .HasOne<Survey>(q => q.Survey)
            .WithMany(s => s.Questions)
            .HasForeignKey(q => q.SurveyId);

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId);
        
        AddIndexes(modelBuilder);
        FillData(modelBuilder);
    }

    private void AddIndexes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Interview>()
            .HasIndex(i => i.PublicId)
            .IsUnique();
        modelBuilder.Entity<Interview>()
            .HasIndex(i => i.SurveyId);

        modelBuilder.Entity<Question>()
            .HasIndex(q => q.PublicId)
            .IsUnique();
        modelBuilder.Entity<Question>()
            .HasIndex(q => new { q.SurveyId, q.OrderId });
    }

    private void FillData(ModelBuilder modelBuilder)
    {
        var survey = new Survey
        {
            Id = 1,
            Title = "Test Survey",
        };

        var question1 = new Question
        {
            Id = 1,
            SurveyId = survey.Id,
            OrderId = 0,
            Text = "Какой тип отдыха вам больше нравится?"
        };

        var answer11 = new Answer
        {
            Id = 1,
            QuestionId = question1.Id,
            Value = "Пляжный отдых"
        };

        var answer12 = new Answer
        {
            Id = 2,
            QuestionId = question1.Id,
            Value = "Походы и природа"
        };

        var answer13 = new Answer
        {
            Id = 3,
            QuestionId = question1.Id,
            Value = "Городские экскурсии"
        };

        var question2 = new Question
        {
            Id = 2,
            SurveyId = survey.Id,
            OrderId = 1,
            Text = "Какой напиток вы предпочитаете по утрам?"
        };

        var answer21 = new Answer
        {
            Id = 4,
            QuestionId = question2.Id,
            Value = "Кофе"
        };

        var answer22 = new Answer
        {
            Id = 5,
            QuestionId = question2.Id,
            Value = "Чай"
        };

        var answer23 = new Answer
        {
            Id = 6,
            QuestionId = question2.Id,
            Value = "Сок"
        };

        var answer24 = new Answer
        {
            Id = 7,
            QuestionId = question2.Id,
            Value = "Вода"
        };

        var question3 = new Question
        {
            Id = 3,
            SurveyId = survey.Id,
            OrderId = 3,
            Text = "Какие жанры фильмов вам нравятся?"
        };

        var answer31 = new Answer
        {
            Id = 8,
            QuestionId = question3.Id,
            Value = "Ужасы"
        };

        var answer32 = new Answer
        {
            Id = 9,
            QuestionId = question3.Id,
            Value = "Комедия"
        };

        var answer33 = new Answer
        {
            Id = 10,
            QuestionId = question3.Id,
            Value = "Фантастика"
        };

        var answer34 = new Answer
        {
            Id = 11,
            QuestionId = question3.Id,
            Value = "Детектив"
        };

        modelBuilder.Entity<Survey>().HasData(survey);
        modelBuilder.Entity<Question>().HasData(question1, question2, question3);
        modelBuilder.Entity<Answer>().HasData(
            answer11, answer12, answer13,
            answer21, answer22, answer23, answer24,
            answer31, answer32, answer33, answer34
        );
    }
}