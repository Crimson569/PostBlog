using AuthService.Domain.Enums;

namespace AuthService.Application.Dto;

public record UserUpdateDto(Guid Id, string UserName, UserRole Role);