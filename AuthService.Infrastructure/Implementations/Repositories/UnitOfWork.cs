using AuthService.Application.Interfaces.Repositories;

namespace AuthService.Infrastructure.Implementations.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AuthServiceDbContext _dbContext;
    public IUserRepository Users { get; }

    public UnitOfWork(IUserRepository users, AuthServiceDbContext dbContext)
    {
        Users = users;
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