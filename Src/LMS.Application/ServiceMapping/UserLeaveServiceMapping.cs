using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Domain.Entities;
using AutoMapper;
using static LMS.Application.Constants.ConstEnum;
using LMS.Application.Interfaces.IServices;

namespace LMS.Application.ServiceMappings;
public class UserLeaveServiceMapping : GenericServiceAsync<UserLeave, UserLeaveDto>, IUserLeaveServiceMapping
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILeaveNotificationService _notification;

    public UserLeaveServiceMapping(IUnitOfWork unitOfWork, IMapper mapper, ILeaveNotificationService notification) : base(unitOfWork, mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
        this._notification = notification;
    }

    
    public List<UserLeaveListDto> GetAllUserLeaveList(int departmentId = 0, int userId = 0)
    {
        var result = this._unitOfWork.UserLeaveRepository.GetAllUserLeaveList(departmentId).Result;

        if (userId > 0)
        {
            result = result.Where(u => u.UserId == userId).ToList();
        }

        var userLeaveResult = _mapper.Map<List<UserLeaveListDto>>(result);

        return userLeaveResult;
    }

    public List<UserLeaveReportDto> GetUserLeaveReport(int departmentId = 0, int userId = 0)
    {
        var report = this._unitOfWork.UserLeaveRepository.GetUserLeaveReport(departmentId).Result;

        if (userId > 0)
            report = report.Where(u => u.UserId == userId).ToList();

        return report;
    }

    public List<UserLeaveDto> ApplyLeave(UserLeaveAddDto dto, List<UserLeaveReportDto> userReport, IEnumerable<LeaveTypeDto> leaveTypes)
    {
        int appliedLeaveCount = 0;
        int currentLeaveCount = 0;
        var userLeaves = new List<UserLeaveDto>();
        int lopLeaveTypeId = 0;
        DateTime fromDate = dto.FromDate;
        DateTime toDate = dto.ToDate;

        if (fromDate.Day < toDate.Day)
            appliedLeaveCount = (toDate - fromDate).Days;
        else
            appliedLeaveCount = (fromDate - toDate).Days;

        if (leaveTypes != null)
        {
            var lopLeaveType = leaveTypes.FirstOrDefault(s => s.LeaveTypeName == LeaveTypeEnum.LOP.ToString());

            if (lopLeaveType != null)
            {
                lopLeaveTypeId = lopLeaveType.Id;
            }
        }
        int? leaveRemainingCount = 0;

        if (userReport != null)
        {
            var leaveDetail = userReport.Where(s => s.LeaveTypeId == dto.LeaveTypeId).FirstOrDefault();
            leaveRemainingCount = leaveDetail?.TotalLeaveRemaining;
            currentLeaveCount = leaveDetail.TotalLeaveTaken;
        }

        for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
        {
            UserLeaveAddDto leave = new();
            leave = dto;
            leave.FromDate = date;
            leave.ToDate = date;
            if (currentLeaveCount > leaveRemainingCount)
            {
                leave.LeaveTypeId = lopLeaveTypeId;
            }
            userLeaves.Add(_mapper.Map<UserLeaveDto>(dto));
            currentLeaveCount++;
        }

        return userLeaves;
    }

    public void SendLeaveAppliedNotification(UserLeaveAddDto dto)
    {
        LeaveAppliedNotificationDto notificationDto;
        notificationDto = _mapper.Map<LeaveAppliedNotificationDto>(dto);
        _notification.LeaveAppliedNotification(notificationDto);
    }

    public void LeaveStatusUpdateNofication(int userLeaveId)
    {
        LeaveStatusNotificationDto statusDto = new (); 
        var userLeave = _unitOfWork.UserLeaveRepository.GetUserLeaveDetail(userLeaveId).Result;
        statusDto = _mapper.Map<LeaveStatusNotificationDto>(userLeave);

        _notification.LeaveStatusUpdateNofication(statusDto);
    }

    
}