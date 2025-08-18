using AuthService.Application.Interfaces.Repositories;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Implementations.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}