using AutoMapper;
using LMS.Domain.Entities;
using LMS.Application.DTOs;

namespace LMS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<AddUserDto, UserDto>().ReverseMap();
    }
}
