using AuthService.Domain.Enums;

namespace AuthService.Application.Dto;

public record UserDto(string UserName, string Email, UserRole Role);