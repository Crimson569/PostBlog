using RabbitMQ.Client;

namespace PostService.Infrastructure.Configurations.RabbitMq;

public class RabbitMqSetupService
{
    private readonly IConnection _connection;
    
    private const string ExchangeName = "users-exchange";
    private const string QueueName = "users-queue";
    private const string RoutingKey = "users.event.deleted";

    public RabbitMqSetupService(IConnection connection)
    {
        _connection = connection;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct, cancellationToken: cancellationToken);
        await channel.QueueDeclareAsync(QueueName, false, false, false, cancellationToken: cancellationToken);
        await channel.QueueBindAsync(QueueName, ExchangeName, RoutingKey, cancellationToken: cancellationToken);
    }
}