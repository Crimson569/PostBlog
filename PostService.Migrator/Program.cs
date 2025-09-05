using Microsoft.EntityFrameworkCore;
using PostService.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await RunMigratorAsync();

static async Task RunMigratorAsync()
{
    var host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration((_, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false)
                  .AddJsonFile("appsettings.local.json", optional: true)
                  .AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            // Регистрируем инфраструктуру, включая DbContext с Npgsql и enum'ами
            services.ConfigureInfrastructureServices(context.Configuration);
        })
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        })
        .Build();

    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var dbContext = services.GetRequiredService<PostService.Infrastructure.PostServiceDbContext>();

        // Проверка соединения
        try
        {
            await dbContext.Database.OpenConnectionAsync();
            await dbContext.Database.CloseConnectionAsync();
            logger.LogInformation("Подключение к базе успешно.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Не удалось подключиться к базе. Проверьте строку подключения и состояние PostgreSQL.");
            throw;
        }

        // Проверяем ожидающие миграции
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Выполняются миграции: {@Migrations}", pendingMigrations);
            await dbContext.Database.MigrateAsync();

            // Перезагрузка типов PostgreSQL (enum и т.д.)
            if (dbContext.Database.GetDbConnection() is Npgsql.NpgsqlConnection npgsqlConnection)
            {
                await npgsqlConnection.OpenAsync();
                try
                {
                    await npgsqlConnection.ReloadTypesAsync();
                }
                finally
                {
                    await npgsqlConnection.CloseAsync();
                }
            }

            logger.LogInformation("Миграции успешно применены.");
        }
        else
        {
            logger.LogInformation("Нет ожидающих миграций.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ошибка при выполнении миграций");
    }
}
