namespace AuthService.Application.Primitives;

public class ApplicationExceptionMessages
{
    public static string PostNotFoundWithId(Guid id) => $"Post with id {id} not found";
}