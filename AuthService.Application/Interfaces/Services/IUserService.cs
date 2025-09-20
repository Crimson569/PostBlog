using AuthService.Application.Collections;
using AuthService.Application.Dto;

namespace AuthService.Application.Interfaces.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<PagedList<UserDto>> GetAllUsers(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken = default);
    Task CreateUser(UserCreateDto userDto, CancellationToken cancellationToken = default);
    Task<string> LoginUser(UserLoginDto userDto, CancellationToken cancellationToken = default);
    Task UpdateUser(UserUpdateDto userDto, CancellationToken cancellationToken = default);
    Task DeleteUser(Guid id, CancellationToken cancellationToken = default);
}