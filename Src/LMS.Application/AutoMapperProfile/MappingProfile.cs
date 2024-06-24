using AutoMapper;
using LMS.Domain.Entities;
using LMS.Application.DTOs;
using LMS.Application.Constants;

namespace LMS.Application.AutoMapper;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, UserDto>();
        CreateMap<AddUserDto, UserDto>().ReverseMap();
        //Leave Type
        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        //User Leave
        CreateMap<UserLeave, UserLeaveDto>().ReverseMap();
        CreateMap<UserLeaveDto, UserLeave>().ReverseMap();
        CreateMap<UserLeaveDto, UserLeaveAddDto>().ReverseMap();
        CreateMap<UserLeaveListDto, UserLeaveDto>().ReverseMap();
        CreateMap<UserLeave, UserLeaveListDto>()
        .ForPath(s => s.LeaveTypeName, opt => opt.MapFrom(src => src.LeaveType.LeaveTypeName))
        .ForPath(s => s.StatusName, opt => opt.MapFrom(src => ((ConstEnum.LeaveStatus)src.Status).ToString()))
        .ForPath(s => s.Name, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
        CreateMap<User,LoginResultDto>()
        .ForPath(s => s.RolePrivilege, opt => opt.MapFrom(src =>src.Role.RolePrivilege))
        .ForPath(s => s.Department, opt => opt.MapFrom(src =>src.Department));
        CreateMap<Role, RoleDto>();
        CreateMap<RolePrivilege,RolePrivilegeDto>();
        //Department
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentDto, Department>();

        //Email notification
        CreateMap<UserLeaveAddDto, LeaveAppliedNotificationDto>();
        CreateMap<LeaveAppliedNotificationDto, EmailDto>();
        CreateMap<LeaveStatusNotificationDto, EmailDto>();       
    }
}
