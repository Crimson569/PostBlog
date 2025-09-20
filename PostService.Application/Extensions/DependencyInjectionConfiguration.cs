using Microsoft.Extensions.DependencyInjection;
using PostService.Application.Interfaces.Services;

namespace PostService.Application.Extensions;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjectionConfiguration).Assembly);

        return services;
    }
}