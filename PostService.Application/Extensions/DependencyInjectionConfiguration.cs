using Microsoft.Extensions.DependencyInjection;
using PostService.Application.Interfaces.Services;
using PostService.Application.Services;

namespace PostService.Application.Extensions;
public static class DependencyInjectionConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjectionConfiguration).Assembly);

        services.AddScoped<IPostManager, PostManager>();

        return services;
    }
}