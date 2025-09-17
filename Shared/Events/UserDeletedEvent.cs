namespace Shared.Events;

public class UserDeletedEvent
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}