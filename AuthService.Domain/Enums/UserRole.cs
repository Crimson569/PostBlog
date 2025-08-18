namespace AuthService.Domain.Enums;

public enum UserRole
{
    /// <summary>
    /// Читатель
    /// </summary>
    Reader = 0,
    
    /// <summary>
    /// Писатель
    /// </summary>
    Writer = 1,
    
    /// <summary>
    /// Администратор
    /// </summary>
    Admin = 2
}