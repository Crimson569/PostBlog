using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Application.Interfaces.Auth;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Configurations.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    
    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public string GenerateToken(User user)
    {
        Claim[] claims = [new("userId", user.Id.ToString())];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));
        
        var tokenVlaue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenVlaue;
    }
}

