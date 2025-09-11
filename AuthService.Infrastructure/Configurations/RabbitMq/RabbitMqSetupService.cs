using RabbitMQ.Client;

namespace AuthService.Infrastructure.Configurations.RabbitMq;

public class RabbitMqSetupService
{
    private const string ExchangeName = "users-exchange";
    private const string QueueName = "users-queue";
    private const string RoutingKey = "users.event.deleted";
    
    private readonly IConnection _connection;

    public RabbitMqSetupService(IConnection connection)
    {
        _connection = connection;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        
        await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct, cancellationToken: cancellationToken);
        await channel.QueueDeclareAsync(QueueName, false, false, false, null, cancellationToken: cancellationToken);
        await channel.QueueBindAsync(QueueName, ExchangeName, RoutingKey, cancellationToken: cancellationToken);
    }
}