namespace AuthService.Application.Primitives;

public class ApplicationExceptionMessages
{
    public const string WrongPassword = "Wrong password";
    public static string UserNotFoundWithId(Guid id) => $"User with id {id} not found";
    public static string UserNotFoundWithUsername(string username) => $"User with username {username} not found";
    public static string UserAlreadyExistsWithUsername(string username) => $"User with username {username} already exists";
}