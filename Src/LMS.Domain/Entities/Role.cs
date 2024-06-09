using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Entities;
public class Role : BaseEntity
{
    [Required(ErrorMessage = "Role name is required")]
    [StringLength(60, ErrorMessage = "Role name length can't exceed 60 characters")]
    public string RoleName { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Description can't exceed 200 characters")]
    public string? Description { get; set; }

    public ICollection<RolePrivilege>? RolePrivilege { get; set; }
}
