namespace AuthService.Application.Dto;

public record UserCreateDto(string UserName, string Email, string Password);