using AuthService.Application.Dto;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Services;
using AutoMapper;

namespace AuthService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync(cancellationToken));
    }

    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(id, cancellationToken));
    }

    public Task CreateUser(UserCreateDto userDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUser(UserUpdateDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userDto.Id, cancellationToken);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        _mapper.Map(userDto, user);
        
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}