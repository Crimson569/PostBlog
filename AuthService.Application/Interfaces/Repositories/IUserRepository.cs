using AuthService.Application.Dto;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default);
}