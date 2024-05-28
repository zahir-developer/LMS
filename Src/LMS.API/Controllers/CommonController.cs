using Microsoft.AspNetCore.Mvc;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IServiceMappings;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommonController : ControllerBase
{
    private readonly ILogger<CommonController> _logger;

    private readonly ILeaveTypeService _leaveTypeService;
    private readonly IRoleService _roleService;
    

    public CommonController(ILogger<CommonController> logger, ILeaveTypeService leaveTypeService, IRoleService roleService)
    {
        _logger = logger;
        _leaveTypeService = leaveTypeService;
        _roleService = roleService;
    }

    [HttpGet]
    [Route("LeaveTypes")]
    public async Task<ActionResult<List<LeaveTypeDto>>> LeaveType()
    {
        var result = await _leaveTypeService.GetAllAsync();

        return result.ToList();
    }

    [HttpGet]
    [Route("Roles")]
    public async Task<ActionResult<List<RoleDto>>> Roles()
    {
        var result = await _roleService.GetAllAsync();
        return result.ToList();
    }

}
