namespace AuthService.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}