using System.Net.Mime;
using AuthService.Application.Interfaces.RabbitMq;
using PostService.Application.Dto;
using RabbitMQ.Client;

namespace PostService.Infrastructure.Implementations.RabbitMq;

public class UserDeletedEventPublisher : IUserDeletedEventPublisher
{
    private const string ExchangeName = "users-exchange";
    private const string QueueName = "users-queue";
    private const string RoutingKey = "users.event.deleted";
    
    private readonly IConnection _connection;

    public UserDeletedEventPublisher(IConnection connection)
    {
        _connection = connection;
    }


    public async Task PublishAsync(UserDeletedEvent @event, CancellationToken cancellationToken = default)
    {
        var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        var message = $"{@event.Id}";
        var messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);
        var props = new BasicProperties();

        await channel.BasicPublishAsync(ExchangeName, RoutingKey, false, props, messageBodyBytes, cancellationToken);
    }
}