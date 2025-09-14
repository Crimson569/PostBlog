namespace AuthService.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository Users { get; } 
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}