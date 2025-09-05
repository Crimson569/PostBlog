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
}