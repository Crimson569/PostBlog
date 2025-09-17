namespace Shared.Events;

public record UserCreatedEvent(Guid Id, string Username, string Email);
