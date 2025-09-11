using AuthService.Application.Interfaces.Auth;
using AuthService.Application.Interfaces.RabbitMq;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Domain.Enums;
using AuthService.Infrastructure.Implementations.Auth;
using AuthService.Infrastructure.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Application.Interfaces.RabbitMq;
using PostService.Infrastructure.Implementations.RabbitMq;

namespace AuthService.Infrastructure.Extensions;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AuthServiceDbContext>((serviceProvider, options) =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString =  config.GetConnectionString("DefaultConnection");
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(AuthServiceDbContext).Assembly.GetName().Name);
                npgsqlOptions.MapEnum<UserRole>();
            });
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserDeletedEventPublisher, UserDeletedEventPublisher>();
        
        return services;
    }
}