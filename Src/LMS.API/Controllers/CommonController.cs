using Microsoft.AspNetCore.Mvc;

using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommonController : ControllerBase
{
    private readonly ILogger<CommonController> _logger;

    private readonly ILeaveTypeServiceMapping _leaveTypeService;
    private readonly IRoleService _roleService;
    

    public CommonController(ILogger<CommonController> logger, ILeaveTypeServiceMapping leaveTypeService, IRoleService roleService)
    {
        _logger = logger;
        _leaveTypeService = leaveTypeService;
        _roleService = roleService;
    }

    [HttpGet]
    [Route("roles")]
    public async Task<ActionResult<List<RoleDto>>> Roles()
    {
        var result = await _roleService.GetAllAsync();
        return result.ToList();
    }

    /*
    /// <summary>
    /// Send email notification
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("SendEmail")]
    public async Task<ActionResult<bool>> SendEmailNotification()
    {
        EmailDto email = new();
        email.From = "zahir.aspire@gmail.com";
        email.To = "zahir.aspire@gmail.com";
        //zahir.aspire@gmail.com
        //zahir.developer@live.com
        email.Subject = "Test";
        email.DisplayNameSender = "LMS Admin";
        _emailService.SendEmail(email);

        return true;
    }
    */

}
