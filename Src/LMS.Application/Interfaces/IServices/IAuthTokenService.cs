using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Application.DTOs;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace LMS.Application.Interfaces.IServices;
public interface IAuthTokenService
{
    string GenerateToken(string emailId, string? roleName = null);
    UserDto RegisterAuthUser(AddUserDto userDto);
    LoginResultDto ValidateUser(LoginDto loginDto);
}