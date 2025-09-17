namespace Shared.Events;

public record UserUpdatedEvent(Guid Id, string Username, string Email);