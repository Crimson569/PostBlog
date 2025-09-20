using AuthService.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application.Extensions;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjectionConfiguration).Assembly);
        
        return services;
    }
}