using Application.Interfaces.Repositories;
using Domain;

namespace Infrastructure.Repositories;

public class ResultRepository(AppDbContext context) : IResultRepository
{
    public void AddResult(Result result) => context.Results.Add(result);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}