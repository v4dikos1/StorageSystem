using Application.Common.Abstractions;

namespace Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly StorageServiceDbContext _dbContext;

    public UnitOfWork(StorageServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    { 
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}