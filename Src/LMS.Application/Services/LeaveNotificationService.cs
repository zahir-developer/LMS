using AutoMapper;
using LMS.Application.Constants;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Application.IServiceMappings;
using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application;

public class LeaveNotificationService : ILeaveNotificationService
{
    private readonly IUserServiceMapping _userService;
    private readonly IEmailService _emailService;
    private readonly ILeaveTypeServiceMapping _leaveTypeService;
    private readonly IMapper _mapper;

    public LeaveNotificationService(IUserServiceMapping userService, IEmailService emailService, ILeaveTypeServiceMapping leaveTypeService, IMapper mapper)
    {
        this._userService = userService;
        this._emailService = emailService;
        this._leaveTypeService = leaveTypeService;
        this._mapper = mapper;
    }

    public void LeaveAppliedNotification(LeaveAppliedNotificationDto notificationDto)
    {
        string leaveTypeName = string.Empty;
        var manager = _userService.GetManagerByDepartment(notificationDto.DepartmentId).Result;
        if (manager != null)
            notificationDto.Email = manager.Email;

        var leaveType = _leaveTypeService.GetByIdAsync(notificationDto.LeaveTypeId).Result;

        if(leaveType != null)
        {
            leaveTypeName = leaveType.LeaveTypeName;
        }
        
        if (manager != null)
            notificationDto.FirstName = manager.FirstName;

        var emailDto = _mapper.Map<EmailDto>(notificationDto);
        emailDto.EmailType = EmailHtmlTemplate.LeaveApplied;
        emailDto.Subject = "LMS - " + EmailHtmlTemplate.LeaveApplied;
        emailDto.Email = "izahirhussain@live.com";

        emailDto.EmailKeyValues = new Dictionary<string, string>(){
            {"USER_NAME", notificationDto.FirstName },
            {"LEAVE_FROM", notificationDto.FromDate.ToString(ConstEnum.DATE_FORMAT)},
            {"LEAVE_TO", notificationDto.ToDate.ToString(ConstEnum.DATE_FORMAT)},
            {"LEAVE_TYPE", leaveTypeName},
        };
        _emailService.SendEmail(emailDto);
    }

    public void LeaveStatusUpdateNofication(LeaveStatusNotificationDto leaveStatusDto)
    {
        var user = _userService.GetByIdAsync(leaveStatusDto.UserId).Result;
        var emailDto = _mapper.Map<EmailDto>(leaveStatusDto);
    }
}
