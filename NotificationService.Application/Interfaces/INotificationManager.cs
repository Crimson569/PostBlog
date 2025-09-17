namespace NotificationService.Application.Interfaces;

public interface INotificationManager
{
    Task SendAsync(CancellationToken cancellationToken = default);
}