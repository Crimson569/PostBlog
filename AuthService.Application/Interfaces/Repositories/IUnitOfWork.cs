namespace AuthService.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; } 
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}