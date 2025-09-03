using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}