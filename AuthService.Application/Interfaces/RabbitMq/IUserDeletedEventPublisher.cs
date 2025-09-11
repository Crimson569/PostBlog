using PostService.Application.Dto;

namespace AuthService.Application.Interfaces.RabbitMq;

public interface IUserDeletedEventPublisher
{
    Task PublishAsync(UserDeletedEvent @event, CancellationToken cancellationToken = default);
}