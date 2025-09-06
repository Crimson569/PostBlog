namespace PostService.Application.Exceptions;

public class PostNotFoundException : NotFoundException
{
    public PostNotFoundException(string message) : base(message)
    {
    }

    public PostNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}