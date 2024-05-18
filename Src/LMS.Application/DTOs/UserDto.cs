using System.Linq;

namespace LMS.Application.DTOs;
public class UserDto
{
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
