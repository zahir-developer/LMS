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
using LMS.API.Constants;
using LMS.Application.Interfaces.ServiceMappings;
using System.Linq;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveController : ControllerBase
{
    private readonly ILogger<LeaveController> _logger;

    private readonly IUserLeaveService _userLeaveService;

    public LeaveController(ILogger<LeaveController> logger, IUserLeaveService userLeaveService, IAuthTokenService authTokenService)
    {
        _logger = logger;
        _userLeaveService = userLeaveService;
    }

    [HttpGet]
    [Route("UserLeave")]
    public async Task<ActionResult<List<UserLeaveDto>>> GetAllUserLeaves()
    {
        return _userLeaveService.GetAllAsync().Result.ToList();
    }

    [HttpPost]
    [Route("UserLeave")]
    public async Task<ActionResult<bool>> AddUserLeave(UserLeaveDto dto)
    {
        _userLeaveService.AddAsync(dto);

        return _userLeaveService.SaveAsync().Result;
    }

}
