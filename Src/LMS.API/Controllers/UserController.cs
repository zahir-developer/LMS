using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using AutoMapper;
using LMS.Application.Helpers.Pagination;

namespace LMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserServiceMapping _userService;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;
    private readonly IAuthTokenService _authTokenService;

    public UserController(IUserServiceMapping userService, IMapper mapper, IAuthTokenService authTokenService)
    {
        _userService = userService;
        _authTokenService = authTokenService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize("User_View_All")]
    [Route("users")]
    public async Task<IActionResult> GetAllUser([FromQuery]UserParams userParams)
    {
        var result = _userService.GetAllUserListAsync(userParams).Result;

        return Ok(result);
    }

    /// <summary>
    /// Updates user details. Policy only admin can access.
    /// </summary>
    /// <param name="statusUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize("User_Edit_Update")]
    public async Task<ActionResult<bool>> UpdateUser(UserUpdateDto userUpdateDto)
    {
        var user = await _userService.GetByIdAsync(userUpdateDto.Id);

        var userUpdated = _mapper.Map(userUpdateDto, user);

        await _userService.UpdateAsync(userUpdated);
        return _userService.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes the user with given userId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize("User_Delete")]
    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> DeleteUser(int userId)
    {
        if (userId > 0)
        {
            await _userService.DeleteByIdAsync(userId);
            return _userService.SaveChangesAsync();
        }
        return false;
    }

    /// <summary>
    /// Checks if emailId already availabe or not
    /// </summary>
    /// <param name="emailId"></param>
    /// <returns></returns>
    [Authorize("User_Email_Exists")]
    [HttpGet("{emailId}")]
    public async Task<ActionResult<bool>> checkEmailExists(string emailId)
    {
        var user = _userService.GetUserByEmail(emailId).Result;

        return user != null;
    }
}
