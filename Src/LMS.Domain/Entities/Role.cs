using System.Collections;

namespace LMS.Domain.Entities;
public class Role : BaseEntity
{
    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public ICollection<RolePrivilege>? RolePrivilege { get; set; }
}
