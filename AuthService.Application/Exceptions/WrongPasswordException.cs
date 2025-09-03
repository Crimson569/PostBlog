namespace AuthService.Application.Exceptions;

public class WrongPasswordException : WrongDataException
{
    public WrongPasswordException(string message) : base(message)
    {
    }

    public WrongPasswordException(string message, Exception innerException) : base(message, innerException)
    {
    }
}