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

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserServiceMapping _userService;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthTokenService _authTokenService;

    public UserController(ILogger<UserController> logger, IUserServiceMapping userService, IConfiguration config, IAuthTokenService authTokenService)
    {
        _logger = logger;
        _userService = userService;
        _config = config;
        _authTokenService = authTokenService;
    }

    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<List<UserDto>>> GetAllUser()
    {
        var result = _userService.GetAllAsync().Result.ToList();

        return result;
    }
}
