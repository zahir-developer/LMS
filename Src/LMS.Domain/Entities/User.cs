namespace LMS.Domain.Entities;
using System.Collections;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt {get; set; }
    public int RoleId { get; set; }
    public int? DepartmentId { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<UserLeave>? UserLeave { get; set; }
    public virtual Department Department { get; set; }
}