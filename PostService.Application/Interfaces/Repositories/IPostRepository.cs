using System.Linq.Expressions;
using PostService.Domain.Entities;

namespace PostService.Application.Interfaces.Repositories;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<List<Post>> GetPostsByFilterAsync(Expression<Func<Post, bool>> filter,
        CancellationToken cancellationToken = default);
}