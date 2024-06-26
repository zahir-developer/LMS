using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IUserServiceMapping _userService;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthTokenService _authTokenService;

    public AuthController(ILogger<AuthController> logger, IUserServiceMapping userService, IConfiguration config, IAuthTokenService authTokenService)
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
    [Route("signup")]
    [Authorize("User_Signup")]
    public async Task<ActionResult<UserDto>> RegisterUser([FromBody] AddUserDto user)
    {
        _logger.LogInformation("User {0} create begins", user.Email);

        var userAuth = _authTokenService.RegisterAuthUser(user);
        if (userAuth != null)
            _logger.LogInformation("User {0} created successfully ", user.Email);
            
        return userAuth;
    }

    /// <summary>
    /// Validates login credentails and generates JWT token
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<ActionResult<LoginResultDto>> Login(LoginDto login)
    {
        var result = _authTokenService.ValidateUser(login);

        if (result?.Id == 0)
        {
            return Unauthorized("Unauthorized user.");
        }
        else
        {
            return result;
        }
    }

    /// <summary>
    /// Validates token and generates token and refresh token
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("refreshToken")]
    public async Task<ActionResult<AuthTokenDto>> RefreshToken(AuthTokenDto refreshToken)
    {
        var result = _authTokenService.RefreshToken(refreshToken);

        if (result.RefreshToken == string.Empty)
        {
            return Unauthorized("Unauthorized user.");
        }
        else
        {
            return result;
        }
    }
}
