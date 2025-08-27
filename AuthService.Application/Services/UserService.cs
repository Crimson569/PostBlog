using AuthService.Application.Dto;
using AuthService.Application.Interfaces.Auth;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Services;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AutoMapper;

namespace AuthService.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync(cancellationToken));
    }

    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(id, cancellationToken));
    }

    public async Task CreateUser(UserCreateDto userDto, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.Generate(userDto.Password);
        
        var user = new User(userDto.UserName, hashedPassword, UserRole.Reader);
        
        await _userRepository.CreateAsync(user, cancellationToken);
    }

    public async Task<string> LoginUser(UserLoginDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsername(userDto.UserName, cancellationToken);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = _passwordHasher.Verify(userDto.Password, user.PasswordHash);

        if (!result)
        {
            throw new Exception("Invalid password");
        }
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