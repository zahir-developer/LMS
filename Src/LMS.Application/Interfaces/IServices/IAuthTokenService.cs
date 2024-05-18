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
    string GenerateToken(string emailId);
    UserDto RegisterAuthUser(AddUserDto userDto);
    AuthTokenDto ValidateUser(LoginDto loginDto);
}
