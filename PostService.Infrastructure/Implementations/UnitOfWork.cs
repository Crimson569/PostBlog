using PostService.Application.Interfaces.Repositories;

namespace PostService.Infrastructure.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostServiceDbContext _dbContext;
    public IPostRepository Posts { get; }

    public UnitOfWork(IPostRepository posts, PostServiceDbContext dbContext)
    {
        Posts = posts;
        _dbContext = dbContext;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}