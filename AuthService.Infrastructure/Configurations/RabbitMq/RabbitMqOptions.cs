namespace AuthService.Infrastructure.Configurations.RabbitMq;

public class RabbitMqOptions
{
    public string HostName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = string.Empty;
}