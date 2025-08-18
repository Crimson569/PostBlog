using AuthService.Domain.Enums;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Primitives;

namespace AuthService.Domain.Entities;

public class User : BaseEntity<User>
{

    public User(string userName, string password, UserRole role)
    {
        UserName = userName;
        Password = password;
        Role = role;
    }
    
    private User()
    {
    }
    
    public string UserName
    {
        get => _userName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException(ExceptionMessages.UserNameEmpty);
            }

            if (value.Length < Constants.MinUserNameLength)
            {
                throw new DomainValidationException(ExceptionMessages.UserNameTooShort, Constants.MinUserNameLength);
            }

            if (value.Length > Constants.MaxUserNameLength)
            {
                throw new DomainValidationException(ExceptionMessages.UserNameTooLong, Constants.MaxUserNameLength);
            }
            
            _userName = value;
        }
    }
    
    private string _userName;

    public string Password
    {
        get => _password;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException(ExceptionMessages.PasswordEmpty);
            }

            if (value.Length < Constants.MinPasswordLength)
            {
                throw new DomainValidationException(ExceptionMessages.PasswordTooShort, Constants.MinPasswordLength);
            }

            if (value.Length > Constants.MaxPasswordLength)
            {
                throw new DomainValidationException(ExceptionMessages.PasswordTooLong, Constants.MaxPasswordLength);
            }
            
            _password = value;
        }
    }
    
    private string _password;

    public UserRole Role
    {
        get => _role;
        private set
        {
            if (!Enum.IsDefined(typeof(UserRole), value))
            {
                throw new DomainValidationException(ExceptionMessages.InvalidUserRole);
            }
            
            _role = value;
        }
    }
    
    private UserRole _role;
}