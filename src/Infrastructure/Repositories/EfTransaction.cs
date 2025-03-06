using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public sealed class EfTransaction(IDbContextTransaction transaction) : ITransaction, IAsyncDisposable
{
    public void Dispose()
    {
        transaction.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await transaction.DisposeAsync();
    }

    public async Task CommitAsync()
    {
        await transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await transaction.RollbackAsync();
    }
}