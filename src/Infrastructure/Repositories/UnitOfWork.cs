using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class UnitOfWork(
    AppDbContext context,
    IInterviewRepository interviewRepository,
    IQuestionRepository questionRepository,
    IResultRepository resultRepository
) : IUnitOfWork
{
    public IInterviewRepository InterviewRepository => interviewRepository;
    public IQuestionRepository QuestionRepository => questionRepository;
    public IResultRepository ResultRepository => resultRepository;

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public async Task<ITransaction> BeginTransactionAsync() =>
        new EfTransaction(await context.Database.BeginTransactionAsync());
}