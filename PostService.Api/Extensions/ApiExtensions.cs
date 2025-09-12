using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostService.Infrastructure.Configurations.Auth;
using PostService.Infrastructure.Configurations.RabbitMq;
using PostService.Infrastructure.Implementations.Consumers;

namespace PostService.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", 
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите токен в формате: Bearer {токен}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer", 
                        Type = ReferenceType.SecurityScheme
                    },
                },
                new List<string>()
            }});
        });

        services.AddAuthorization();
        
        return services;
    }

    public static IServiceCollection AddApiMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMqOptions"));

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserDeletedEventConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                
                cfg.Host(options.HostName, c =>
                {
                    c.Username(options.Username);
                    c.Password(options.Password);
                });
                
                cfg.ReceiveEndpoint("UserDeletedEventQueue", e =>
                {
                    e.ConfigureConsumer<UserDeletedEventConsumer>(context);
                });
                
                cfg.ClearSerialization();
                cfg.AddRawJsonSerializer();
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}