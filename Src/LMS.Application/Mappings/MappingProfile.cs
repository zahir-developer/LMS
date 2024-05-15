using AutoMapper;
using LMS.Domain.Entities;
using LMS.Application.DTO;

namespace LMS.Application.Mappings;

public class MappingProfile : Profile
{
    MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
