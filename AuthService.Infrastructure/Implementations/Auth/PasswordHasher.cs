using AuthService.Application.Interfaces.Auth;

namespace AuthService.Infrastructure.Implementations.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    
}