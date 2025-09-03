using AuthService.Domain.Enums;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Primitives;

namespace AuthService.Domain.Entities;

public class User : BaseEntity<User>
{
    public string PasswordHash { get; private set; }
    //public string PasswordSalt { get; private set; }

    public User(string userName, string passwordHash, UserRole role)
    {
        UserName = userName;
        Role = role;
        PasswordHash = passwordHash;
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