namespace LMS.Application.DTOs;

public class RolePrivilegeDetailDto
{
    private int RoleId;
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    private List<RolePrivilegeDto> RolePrivilege { get; set; }
}
