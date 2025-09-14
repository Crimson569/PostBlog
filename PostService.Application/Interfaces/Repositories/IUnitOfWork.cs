namespace PostService.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IPostRepository Posts { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}