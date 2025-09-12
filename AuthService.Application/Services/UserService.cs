using AuthService.Application.Dto;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces.Auth;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Services;
using AuthService.Application.Primitives;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AutoMapper;
using MassTransit;
using Shared.Events;

namespace AuthService.Application.Services;

public class UserService : IUserService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken)
    {
        return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync(cancellationToken));
    }

    public async Task<UserDto> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(id, cancellationToken));

        if (user == null)
        {
            throw new UserNotFoundException(ApplicationExceptionMessages.UserNotFoundWithId(id));
        }
        
        return user;
    }

    public async Task CreateUser(UserCreateDto userDto, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.FindUserByUsername(userDto.UserName, cancellationToken);

        if (userExists)
        {
            throw new UserAlreadyExistsException(ApplicationExceptionMessages.UserAlreadyExistsWithUsername(userDto.UserName));
        }
        
        var hashedPassword = _passwordHasher.Generate(userDto.Password);
        
        var user = new User(userDto.UserName, hashedPassword, UserRole.Reader);
        
        await _userRepository.CreateAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<string> LoginUser(UserLoginDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsername(userDto.UserName, cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(ApplicationExceptionMessages.UserNotFoundWithUsername(userDto.UserName));
        }
        
        var result = _passwordHasher.Verify(userDto.Password, user.PasswordHash);

        if (!result)
        {
            throw new WrongPasswordException(ApplicationExceptionMessages.WrongPassword);
        }
        
        var token = _jwtProvider.GenerateToken(user);
        return token;
    }

    public async Task UpdateUser(UserUpdateDto userDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userDto.Id, cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(ApplicationExceptionMessages.UserNotFoundWithId(userDto.Id));
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
            throw new UserNotFoundException(ApplicationExceptionMessages.UserNotFoundWithId(id));
        }
        
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish<UserDeletedEvent>(new UserDeletedEvent
        {
            Id = user.Id,
            UserName = user.UserName
        }, cancellationToken);
    }
}