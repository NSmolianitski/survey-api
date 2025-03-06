namespace Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IInterviewRepository InterviewRepository { get; }
    IQuestionRepository QuestionRepository { get; }
    IResultRepository ResultRepository { get; }

    Task<int> SaveChangesAsync();
    Task<ITransaction> BeginTransactionAsync();
}