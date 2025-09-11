using AuthService.Api.Extensions;
using AuthService.Application.Extensions;
using AuthService.Infrastructure.Configurations.Auth;
using AuthService.Infrastructure.Configurations.RabbitMq;
using AuthService.Infrastructure.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SupportService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var config = sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
    return new ConnectionFactory
    {
        HostName = config.HostName,
        UserName = config.UserName,
        Password = config.Password,
        VirtualHost = config.VirtualHost,
    };
});

builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = sp.GetRequiredService<IConnectionFactory>();
    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
});

builder.Services.AddSingleton<RabbitMqSetupService>();
builder.Services.AddHostedService<RabbitMqSetupHostedService>();

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddControllers();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthorization();

app.UseAuthorization();

app.Run();