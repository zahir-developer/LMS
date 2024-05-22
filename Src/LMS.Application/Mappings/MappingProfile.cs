using AutoMapper;
using LMS.Domain.Entities;
using LMS.Application.DTOs;

namespace LMS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<UserDto, User>().ReverseMap();
        //.ForMember(dest => dest.PasswordHash, act => act.Ignore())
        //.ForMember(dest => dest.PasswordSalt, act => act.Ignore());
        CreateMap<AddUserDto, UserDto>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<UserLeave, UserLeaveDto>().ReverseMap();
    }
}
