using AutoMapper;
using PostService.Application.Dto;
using PostService.Domain.Entities;

namespace PostService.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PostDto, Post>().ReverseMap();
        CreateMap<PostCreateUpdateDto, Post>().ReverseMap();
    }
}