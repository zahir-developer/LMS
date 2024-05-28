using System.Linq;

namespace LMS.Application.DTOs;
public class UserDto : AddUserDto
{
    public int Id { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Token { get; set; }
}
