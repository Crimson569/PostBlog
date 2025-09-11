using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostService.Application.Interfaces.Repositories;
using PostService.Domain.Entities;

namespace PostService.Infrastructure.Implementations;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    private readonly PostServiceDbContext _dbContext;
    
    public PostRepository(PostServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Post>> GetPostsByFilter(Expression<Func<Post, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Posts.Where(filter).ToListAsync(cancellationToken);
    }
}