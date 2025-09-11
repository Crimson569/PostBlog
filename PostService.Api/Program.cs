using Microsoft.Extensions.Options;
using PostService.Api.Extensions;
using PostService.Api.Middlewares;
using PostService.Application.Extensions;
using PostService.Infrastructure.Configurations.RabbitMq;
using PostService.Infrastructure.Extensions;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var config = sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
    return new ConnectionFactory
    {
        HostName = config.HostName,
        UserName = config.Username,
        Password = config.Password,
        VirtualHost = config.VirtualHost,
        ClientProvidedName = "service:authservice"
    };
});

builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = sp.GetRequiredService<IConnectionFactory>();
    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
});

builder.Services.AddSingleton<RabbitMqSetupService>();
builder.Services.AddHostedService<RabbitMqSetupHostedService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

