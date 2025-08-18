namespace AuthService.Domain.Primitives;

public class ExceptionMessages
{
    public const string UserNameTooLong = "Имя пользователя не должно превышать {0} символов.";
    public const string UserNameTooShort = "Имя пользователя не должно быть меньше {0} символов.";
    public const string UserNameEmpty = "Имя пользователя не должно быть пустым.";
    public const string PasswordTooLong = "Длина пароля не должна превышать {0} символов.";
    public const string PasswordTooShort = "Длина пароля не должна быть ниже {0} символов.";
    public const string PasswordEmpty = "Пароль не должен быть пустым.";
    public const string InvalidUserRole = "Неверная роль пользователя";
}