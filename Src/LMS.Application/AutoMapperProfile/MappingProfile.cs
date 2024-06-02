using AutoMapper;
using LMS.Domain.Entities;
using LMS.Application.DTOs;

namespace LMS.Application.AutoMapper;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        // CreateMap<Role, RoleDto>().ReverseMap();
        // CreateMap<RoleDto, Role>().ReverseMap();
        //.ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id)

        CreateMap<UserDto, User>().ReverseMap();
        //.ForMember(dest => dest.PasswordHash, act => act.Ignore())
        //.ForMember(dest => dest.PasswordSalt, act => act.Ignore());
        CreateMap<AddUserDto, UserDto>().ReverseMap();
        //Leave Type
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        //User Leave
        CreateMap<UserLeave, UserLeaveDto>().ReverseMap();
        CreateMap<UserLeaveDto, UserLeave>().ReverseMap();
        CreateMap<UserLeaveDto, UserLeaveAddDto>().ReverseMap();
        CreateMap<UserLeaveListDto, UserLeaveDto>().ReverseMap();
        CreateMap<User,LoginResultDto>()
        .ForPath(s => s.RolePrivilege, opt => opt.MapFrom(src =>src.Role.RolePrivilege))
        .ForPath(s => s.Department, opt => opt.MapFrom(src =>src.Department));
        CreateMap<Role, RoleDto>();
        CreateMap<RolePrivilege,RolePrivilegeDto>();
        //Department
        CreateMap<Department, DepartmentDto>();
        //.ForMember(d => d.User.Id, opt => opt.MapFrom(src => src.Id));
        //.ReverseMap()

        //CreateMap<UserLeaveListDto, UserLeaveDto>().ReverseMap();
        //.Include<User, UserDto>()
        //.ForMember(dest => dest.FirstName, opt => opt.User.FirstName)
        //.ForMember(dest => dest.LastName, opt => opt.User.LastName);

        //CreateMap<UserLeaveListDto, UserLeaveDto>().ReverseMap();
        //.Include<LeaveType, LeaveTypeDto>()
        //.ForMember(dest => dest.LeaveTypeName, opt => opt.LeaveType.LeaveTypeName);
        
    }
}
