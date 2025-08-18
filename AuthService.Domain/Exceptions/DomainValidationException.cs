namespace AuthService.Domain.Exceptions;

public class DomainValidationException : Exception
{
    public DomainValidationException(string? message) : base(message)
    {
        
    }

    public DomainValidationException(string messageTemplate, params object[] args) : base(string.Format(messageTemplate, args))
    {
        
    }
}