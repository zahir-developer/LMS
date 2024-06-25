using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServiceMappings;
using AutoMapper;
using static LMS.Application.Constants.ConstEnum;
using LMS.Application;
using LMS.Application.Config;

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
    private readonly IEmailService _emailService;


    public LeaveController(
        IUserLeaveServiceMapping userLeaveService,
        ILeaveTypeServiceMapping leaveTypeService,
        IEmailService emailService,
        IMapper mapper,
        ILogger<LeaveController> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _userLeaveService = userLeaveService;
        _leaveTypeService = leaveTypeService;
        _emailService = emailService;
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
    public async Task<ActionResult<bool>> AddUserLeave(UserLeaveAddDto dto)
    {
        bool result = false;
        /*
        var userReport = _userLeaveService.GetUserLeaveReport(userId: dto.UserId);
        var leaveTypes = _leaveTypeService.GetAllAsync().Result;
        var userLeaves = _userLeaveService.ApplyLeave(dto, userReport, leaveTypes);
        await _userLeaveService.AddRangeAsync(userLeaves);
        result = _userLeaveService.SaveChangesAsync();
        */

        if (true)
            _userLeaveService.SendLeaveAppliedNotification(dto);
            

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

            if(result)
            {
                _userLeaveService.LeaveStatusUpdateNofication(statusUpdateDto.Id);
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
