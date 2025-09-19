using AuthService.Domain.Enums;

namespace AuthService.Application.Dto;

public record UserDto(Guid Id, string UserName, UserRole Role);