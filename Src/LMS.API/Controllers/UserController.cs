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
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthTokenService _authTokenService;

    public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration config, IAuthTokenService authTokenService)
    {
        _logger = logger;
        _userService = userService;
        _config = config;
        _authTokenService = authTokenService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("user")]
    public async Task<ActionResult<UserDto>> RegisterUser([FromBody] AddUserDto user)
    {
        _logger.LogInformation("User {0} create begins", user.Email);

        var userAuth = _authTokenService.RegisterAuthUser(user);
        
        return userAuth;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<ActionResult<AuthTokenDto>> Login(LoginDto login)
    {
        AuthTokenDto tokenDto = new AuthTokenDto();

        var result = _authTokenService.ValidateUser(login);
        if (result == null)
        {
            return Unauthorized("Unauthorized user.");
        }
        else
        {
            return result;
        }

        return Unauthorized("Unauthorized user");
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("users")]
    public async Task<IActionResult> GetAllUser()
    {
        var result = await _userService.GetAllAsync();

        return Ok();
    }
}
