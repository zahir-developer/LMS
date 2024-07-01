namespace LMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class User : BaseEntity
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public byte[] PasswordHash { get; set; }

    [Required(ErrorMessage = "PasswordSalt is required.")]
    public byte[] PasswordSalt {get; set; }

    [Required(ErrorMessage = "RoleId is required.")]
    public int RoleId { get; set; }
    public int? DepartmentId { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<UserLeave>? UserLeave { get; set; }
    public virtual Department? Department { get; set; }
}