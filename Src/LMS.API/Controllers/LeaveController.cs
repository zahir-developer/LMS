using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServiceMappings;
using AutoMapper;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveController : ControllerBase
{
    private readonly ILogger<LeaveController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserLeaveServiceMapping _userLeaveService;

    public LeaveController(ILogger<LeaveController> logger, IUserLeaveServiceMapping userLeaveService, IMapper mapper)
    {
        _logger = logger;
        _userLeaveService = userLeaveService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all the user leaves with status
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserLeaveListDto>>> GetAllUserLeaves()
    {
        var result = _userLeaveService.GetAllUserLeaveList();

        return Ok(result);
    }

    /// <summary>
    /// Retrieves all the leave againts given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{userId}")]
    public async Task<ActionResult<List<UserLeaveListDto>>> GetUserLeaves(int userId)
    {
        var result = _userLeaveService.GetAllUserLeaveList();

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
        return true;
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
        
        if(userLeave != null)
        {
            userLeave.Status = (int)statusUpdateDto.Status;
            await _userLeaveService.UpdateAsync(userLeave);
            //await _userLeaveService.SaveAsync();
        }
        
        return true;
    }
}
