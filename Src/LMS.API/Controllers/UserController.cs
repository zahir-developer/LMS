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
using LMS.Application.Helpers.Pagination;

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
        var user = _userService.GetByIdAsync(userUpdateDto.Id).Result;
        if (user != null)
        {
            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Email = userUpdateDto.Email;
            user.RoleId = userUpdateDto.RoleId;
            await _userService.UpdateAsync(user);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes the user with given userId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize("User_Delete")]
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUser(int userId)
    {
        if (userId > 0)
        {
            await _userService.DeleteByIdAsync(userId);
            return true;
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
        var user = _userService.GetUserByEmail(emailId);

        return user != null;
    }
}
