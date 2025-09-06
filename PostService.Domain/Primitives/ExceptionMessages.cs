namespace PostService.Domain.Primitives;

public class ExceptionMessages
{
    public const string PostTitleTooLong = "Длина названия поста не может превышать {0} символов.";
    public const string PostContentTooLong = "Длина текста поста не может превышать {0} символов.";
    public const string PostTitleTooShort = "Длина названия поста не может быть меньше {0} символов.";
    public const string PostContentTooShort = "Длина текста поста не может быть меньше {0} символов.";
    public const string InvalidAuthorId = "Неверный формат идентификатора автора.";
}