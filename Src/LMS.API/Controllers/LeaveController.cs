using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServiceMappings;
using AutoMapper;
using static LMS.Application.Constants.ConstEnum;
using LMS.Application;
using LMS.Application.Config;
using System.Net;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveController : ControllerBase
{
    private readonly ILogger<LeaveController> _logger;
    private readonly IUserLeaveServiceMapping _userLeaveService;
    private readonly ILeaveTypeServiceMapping _leaveTypeService;
    private readonly IHolidayServiceMapping _holidayService;


    public LeaveController(
        IUserLeaveServiceMapping userLeaveService,
        ILeaveTypeServiceMapping leaveTypeService,
        IHolidayServiceMapping holidayServiceMapping,
        ILogger<LeaveController> logger)
    {
        _logger = logger;
        _userLeaveService = userLeaveService;
        _leaveTypeService = leaveTypeService;
        _holidayService = holidayServiceMapping;
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
    //[Authorize("Leave_Apply")]
    [AllowAnonymous]
    public async Task<ActionResult<ResultDto>> AddUserLeave(UserLeaveAddDto dto)
    {
        ResultDto result = new();

        if (!_holidayService.ValidateLeaveDate(dto.FromDate, dto.ToDate))
        {
            result.StatusCode = HttpStatusCode.Forbidden;
            result.Result = false;
            result.ErrorMessage = "Leaves can't be applied on holiday's";
            return result;
        }

        var userReport = _userLeaveService.GetUserLeaveReport(userId: dto.UserId);
        var leaveTypes = _leaveTypeService.GetAllAsync().Result;
        var userLeaves = _userLeaveService.ApplyLeave(dto, userReport, leaveTypes);
        await _userLeaveService.AddRangeAsync(userLeaves);
        result.Result = _userLeaveService.SaveChangesAsync();

        if (result.Result)
        {
            _userLeaveService.SendLeaveAppliedNotification(dto);
        }

        return result;
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
        bool result = false;
        var userLeave = _userLeaveService.GetByIdAsync(statusUpdateDto.Id).Result;

        if (userLeave != null)
        {
            userLeave.Status = (int)statusUpdateDto.Status;
            await _userLeaveService.UpdateAsync(userLeave);
            result = _userLeaveService.SaveChangesAsync();

            if (result)
            {
                _userLeaveService.LeaveStatusUpdateNofication(statusUpdateDto.Id);
            }
        }

        return true;
    }

    /// <summary>
    /// Updates leave status: Approve/Reject
    /// </summary>
    /// <param name="leaveStatusUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("CancelLeave/{userleaveId}")]
    [Authorize("Leave_Cancel")]
    public async Task<ActionResult<bool>> LeaveStatusUpdate(int userleaveId)
    {
        bool result = false;
        var userLeave = _userLeaveService.GetByIdAsync(userleaveId).Result;

        if (userLeave != null)
        {
            userLeave.Status = (int)LeaveStatus.Cancelled;
            await _userLeaveService.UpdateAsync(userLeave);
            result = _userLeaveService.SaveChangesAsync();

            if (result)
            {
                _userLeaveService.LeaveStatusUpdateNofication(userleaveId);
            }
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
