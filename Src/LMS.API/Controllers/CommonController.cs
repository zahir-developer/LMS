using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IServiceMappings;
using System.Net.Http.Headers;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommonController : ControllerBase
{
    private readonly ILogger<CommonController> _logger;

    private readonly ILeaveTypeService _leaveTypeService;

    public CommonController(ILogger<CommonController> logger, ILeaveTypeService leaveTypeService, IAuthTokenService authTokenService)
    {
        _logger = logger;
        _leaveTypeService = leaveTypeService;
    }

    [HttpGet]
    [Route("LeaveTypes")]
    public async Task<ActionResult<List<LeaveTypeDto>>> LeaveType()
    {
        var result = await _leaveTypeService.GetAllAsync();

        return result.ToList();
    }

}
