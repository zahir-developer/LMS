using System.Linq;

namespace LMS.Application.DTO;
public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set;}
    public string Password { get; set;}
    public string Email { get; set; }
    public int RoleId { get; set; }

}
