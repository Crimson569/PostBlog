using PostService.Application.Dto;

namespace PostService.Application.Interfaces.RabbitMq;

public interface IUserDeletedEventPublisher
{
    Task PublishAsync(UserDeletedEvent @event, CancellationToken cancellationToken = default);
}