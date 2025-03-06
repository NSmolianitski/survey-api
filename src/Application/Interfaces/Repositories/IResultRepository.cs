using Domain;

namespace Application.Interfaces.Repositories;

public interface IResultRepository
{
    void AddResult(Result result);
    Task SaveChangesAsync();
}