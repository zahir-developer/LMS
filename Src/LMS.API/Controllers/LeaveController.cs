using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServiceMappings;
using AutoMapper;
using static LMS.Application.Constants.ConstEnum;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveController : ControllerBase
{
    private readonly ILogger<LeaveController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserLeaveServiceMapping _userLeaveService;

    private readonly ILeaveTypeServiceMapping _leaveTypeService;

    public LeaveController(IUserLeaveServiceMapping userLeaveService, ILeaveTypeServiceMapping leaveTypeService, IMapper mapper, ILogger<LeaveController> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _userLeaveService = userLeaveService;
        _leaveTypeService = leaveTypeService;
    }

    /// <summary>
    /// Retrieves all the user leaves with status
    /// </summary>
    /// <returns></returns>
    [HttpGet("department/{departmentId}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<List<UserLeaveListDto>>> GetAllUserLeaves(int departmentId)
    {
        var result = _userLeaveService.GetAllUserLeaveList(departmentId: departmentId, userId: 0);

        return Ok(result);
    }

    /// <summary>
    /// Retrieves all the leave againts given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("user/{userId}")]
    public async Task<ActionResult<List<UserLeaveListDto>>> GetUserLeaves(int userId)
    {
        var result = _userLeaveService.GetAllUserLeaveList(departmentId: 0, userId: userId);

        return Ok(result);
    }

    /// <summary>
    /// Applies leave for given userId with leave detail
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize("Leave_Apply")]
    public async Task<ActionResult<bool>> AddUserLeave(UserLeaveAddDto dto)
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

        var userReport = _userLeaveService.GetUserLeaveReport(userId: dto.UserId);
        var leaveTypes = _leaveTypeService.GetAllAsync().Result;

        if(leaveTypes != null)
        {
            var lopLeaveType = leaveTypes.FirstOrDefault(s=>s.LeaveTypeName == LeaveTypeEnum.LOP.ToString());

            if(lopLeaveType != null)
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
            UserLeaveAddDto leave = new ();
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
        await _userLeaveService.AddRangeAsync(userLeaves);
        return _userLeaveService.SaveChangesAsync();
        //return true;
    }

    /// <summary>
    /// Updates leave status: Approve/Reject
    /// </summary>
    /// <param name="leaveStatusUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("statusUpdate")]
    [Authorize("Leave_Approve_Reject")]
    public async Task<ActionResult<bool>> LeaveStatusUpdate(LeaveStatusUpdateDto statusUpdateDto)
    {
        var userLeave = _userLeaveService.GetByIdAsync(statusUpdateDto.Id).Result;

        if (userLeave != null)
        {
            userLeave.Status = (int)statusUpdateDto.Status;
            await _userLeaveService.UpdateAsync(userLeave);
            return _userLeaveService.SaveChangesAsync();
        }

        return true;
    }

    [HttpGet("report/user/{userId}")]
    [Authorize("UserLeaveReport")]
    public async Task<List<UserLeaveReportDto>> LeaveReportByUserId(int userId)
    {
        var leaveReport = _userLeaveService.GetUserLeaveReport(departmentId: 0, userId: userId);

        return leaveReport;
    }

    [HttpGet("report/department/{departmentId}")]
    [Authorize("LeaveReport")]
    public async Task<List<UserLeaveReportDto>> LeaveReportByDepartment(int departmentId)
    {
        var leaveReport = _userLeaveService.GetUserLeaveReport(departmentId: departmentId, userId: 0);

        return leaveReport;
    }
}
