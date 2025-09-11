using Microsoft.Extensions.Hosting;

namespace PostService.Infrastructure.Configurations.RabbitMq;

public class RabbitMqSetupHostedService : IHostedService
{
    private readonly RabbitMqSetupService _setup;

    public RabbitMqSetupHostedService(RabbitMqSetupService setup)
    {
        _setup = setup;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _setup.InitializeAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}