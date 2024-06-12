using LMS.Application.DTOs;

namespace LMS.Application.Interfaces.IServices;
public interface IAuthTokenService
{
    string GenerateToken(string emailId, string? roleName = null);
    UserDto RegisterAuthUser(AddUserDto userDto);
    LoginResultDto ValidateUser(LoginDto loginDto);
    public AuthTokenDto RefreshToken(AuthTokenDto authTokenDto);
}
