using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Application.DTO;
using LMS.Domain.Entities;
using LMS.Application.IServiceMappings;

namespace LMS.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserMapping _userService;
    private readonly ILogger<UserController> _logger;


    public UserController(ILogger<UserController> logger, IUserMapping userMapping)
    {
        _logger = logger;
        _userService = userMapping;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(UserDto user)
    {
        _logger.LogInformation("User {0} create begins", user.UserName);
        _userService.GetAllAsync();
        return Ok();
    }

}
