using System.Net;

namespace AuthService.Application.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; }
    
    public AppException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public AppException(string message, int statusCode, Exception innerException) : base(message)
    {
        StatusCode = statusCode;
    }
}