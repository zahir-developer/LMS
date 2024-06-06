using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServiceMappings;
using AutoMapper;
using LMS.Application.Interfaces;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveController : ControllerBase
{
    private readonly ILogger<LeaveController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserLeaveServiceMapping _userLeaveService;

    public LeaveController(IUserLeaveServiceMapping userLeaveService, IMapper mapper, ILogger<LeaveController> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _userLeaveService = userLeaveService;
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
        var result = _userLeaveService.GetAllUserLeaveList(departmentId: 0 , userId: userId);

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
        var userLeaveDto = _mapper.Map<UserLeaveDto>(dto);
        await _userLeaveService.AddAsync(userLeaveDto);
        return _userLeaveService.SaveChangesAsync();
    }

    /// <summary>
    /// Updates leave status: Approve/Reject
    /// </summary>
    /// <param name="leaveStatusUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("StatusUpdate")]
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

    [HttpGet("Report/user/{userId}")]
    [Authorize("UserLeaveReport")]
    public async Task<List<UserLeaveReportDto>> LeaveReportByUserId(int userId)
    {
        var leaveReport = _userLeaveService.GetUserLeaveReport(departmentId: 0, userId: userId);

        return leaveReport;
    }

    [HttpGet("Report/department/{departmentId}")]
    [Authorize("LeaveReport")]
    public async Task<List<UserLeaveReportDto>> LeaveReportByDepartment(int departmentId)
    {
        var leaveReport = _userLeaveService.GetUserLeaveReport(departmentId: departmentId, userId: 0);

        return leaveReport;
    }
}
