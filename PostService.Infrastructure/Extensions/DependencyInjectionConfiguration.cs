using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostService.Application.Interfaces.Repositories;
using PostService.Application.Interfaces.Services;
using PostService.Infrastructure.Implementations;
using PostService.Infrastructure.Services;

namespace PostService.Infrastructure.Extensions;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PostServiceDbContext>((serviceProvider, options) =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(PostServiceDbContext).Assembly.GetName().Name);
            });
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostManager, PostManager>();

        return services;
    }
}