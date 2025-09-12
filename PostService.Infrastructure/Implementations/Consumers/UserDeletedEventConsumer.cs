using MassTransit;
using PostService.Application.Interfaces.Services;
using Shared.Events;

namespace PostService.Infrastructure.Implementations.Consumers;

public class UserDeletedEventConsumer : IConsumer<UserDeletedEvent>
{
    private readonly IPostManager _postManager;

    public UserDeletedEventConsumer(IPostManager postManager)
    {
        _postManager = postManager;
    }

    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        await _postManager.DeleteByAuthorIdAsync(context.Message.Id);
    }
}