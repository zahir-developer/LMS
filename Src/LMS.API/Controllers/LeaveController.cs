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
using LMS.Application.Constants;
using LMS.Application.Interfaces.ServiceMappings;
using System.Linq;
using AutoMapper;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
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

    [HttpGet]
    [Route("UserLeave")]
    public async Task<ActionResult<List<UserLeaveListDto>>> GetAllUserLeaves()
    {
        var result = _userLeaveService.GetAllUserLeaveList();

        return Ok(result);
    }

    [HttpPost]
    [Route("UserLeave")]
    public async Task<ActionResult<bool>> AddUserLeave(UserLeaveAddDto dto)
    {
        var userLeaveDto = _mapper.Map<UserLeaveDto>(dto);
        _userLeaveService.AddAsync(userLeaveDto);

        return true;
    }
}