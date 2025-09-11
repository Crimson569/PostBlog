using Microsoft.Extensions.Hosting;
using PostService.Application.Interfaces.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PostService.Infrastructure.Implementations.RabbitMq;

public class UserDeletedEventConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IPostManager _postManager;
    
    private IChannel? _channel;

    public UserDeletedEventConsumer(IConnection connection, IPostManager postManager)
    {
        _connection = connection;
        _postManager = postManager;
    }

    private const string ExchangeName = "users-exchange";
    private const string QueueName = "users-queue";
    private const string RoutingKey = "users.event.deleted";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

            await _postManager.DeleteByAuthorIdAsync(Guid.Parse(message), stoppingToken);

            await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
        };

        await _channel.BasicConsumeAsync(queue: QueueName, 
            autoAck: false, 
            consumer: consumer, 
            cancellationToken: stoppingToken);
    }
}