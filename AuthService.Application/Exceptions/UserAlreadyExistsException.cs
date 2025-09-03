namespace AuthService.Application.Exceptions;

public class UserAlreadyExistsException : AlreadyExistsException
{
    public UserAlreadyExistsException(string message) : base(message)
    {
    }

    public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}