using System.Linq;

namespace LMS.Application.DTOs;
public class UserListDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}
