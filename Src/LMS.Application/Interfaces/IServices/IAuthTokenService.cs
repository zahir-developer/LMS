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
    string GenerateToken(AddUserDto userDto);
    UserDto RegisterAuthUser(AddUserDto userDto);
    string ValidateUser(LoginDto loginDto);
}
