namespace AuthService.Application.Exceptions;

public class WrongDataException : AppException
{
    public WrongDataException(string message) : base(message, 401)
    {
    }

    public WrongDataException(string message, Exception innerException) : base(message, 401, innerException)
    {
    }
}