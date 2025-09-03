using AuthService.Domain.Enums;

namespace AuthService.Application.Dto;

public record UserDto(string UserName, UserRole Role);