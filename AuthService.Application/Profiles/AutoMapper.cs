using AuthService.Application.Dto;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AutoMapper;

namespace AuthService.Application.Profiles;

public class AutoMapper :  Profile
{
    public AutoMapper()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserCreateDto, User>().ForMember(u => u.Role, opt => opt.MapFrom(src => UserRole.Reader));
        CreateMap<UserUpdateDto, User>();
    }
}