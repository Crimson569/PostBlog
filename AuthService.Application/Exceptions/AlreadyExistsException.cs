namespace AuthService.Application.Exceptions;

public class AlreadyExistsException : AppException
{
    public AlreadyExistsException(string message) : base(message, 409)
    {
    }

    public AlreadyExistsException(string message, Exception innerException) : base(message, 409, innerException)
    {
    }
}