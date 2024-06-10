using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Entities;

public class RolePrivilege : BaseEntity
{
    [Required(ErrorMessage = "Privilege name is required")]
    [StringLength(60, ErrorMessage = "Privilege name length can't exceed 60 characters")]
    public string PrivilegeName { get; set; } = string.Empty;

    [StringLength(60, ErrorMessage = "Description length can't exceed 60 characters")]
    public string? Description { get; set; }
    public int RoleId { get; set; }
}
