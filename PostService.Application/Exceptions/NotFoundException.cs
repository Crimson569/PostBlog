using PostService.Application.Exceptions;

namespace PostService.Application.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message, 404)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, 404, innerException)
    {
    }
}