namespace LMS.Domain.Entities;

public class RolePrivilege : BaseEntity
{
    public string PrivilegeName { get; set; }
    public string? Description { get; set; }
    public int RoleId { get; set; }
}
