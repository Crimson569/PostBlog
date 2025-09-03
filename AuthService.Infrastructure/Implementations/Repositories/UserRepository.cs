using AuthService.Application.Dto;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Implementations.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AuthServiceDbContext _dbContext;
    public UserRepository(AuthServiceDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => EF.Functions.ILike(u.UserName, username), cancellationToken);

    }

    public async Task<bool> FindUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.UserName == username, cancellationToken);
    }
}