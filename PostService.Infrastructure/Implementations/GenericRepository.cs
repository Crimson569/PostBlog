using Microsoft.EntityFrameworkCore;
using PostService.Application.Collections;
using PostService.Application.Interfaces.Repositories;

namespace PostService.Infrastructure.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly PostServiceDbContext _dbContext;

    public GenericRepository(PostServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<PagedList<T>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        return await PagedList<T>.CreateAsync(query, page, pageSize);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}