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

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthTokenService _authTokenService;

    public AuthController(ILogger<AuthController> logger, IUserService userService, IConfiguration config, IAuthTokenService authTokenService)
    {
        _logger = logger;
        _userService = userService;
        _config = config;
        _authTokenService = authTokenService;
    }

    /*
    [HttpPost]
    [Route("AddAdmin")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> RegisterAdmin([FromBody] AddUserDto user)
    {
        _logger.LogInformation("Resister Admin begins {0} create begins", user.Email);

        user.RoleId = (int)ConstEnum.Roles.Admin;
        var userAuth = _authTokenService.RegisterAuthUser(user);
        
        return userAuth;
    }
    */
    /// <summary>
    /// Register user detail for signup 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Signup")]
    public async Task<ActionResult<UserDto>> RegisterUser([FromBody] AddUserDto user)
    {
        _logger.LogInformation("User {0} create begins", user.Email);

        user.RoleId = (int)ConstEnum.Roles.User;

        var userAuth = _authTokenService.RegisterAuthUser(user);
        
        return userAuth;
    }

    /// <summary>
    /// Validates login credentails and generates JWT token
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
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
    }
}
