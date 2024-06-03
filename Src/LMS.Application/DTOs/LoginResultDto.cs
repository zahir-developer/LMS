namespace LMS.Application.DTOs;

public class LoginResultDto : UserDto
{
    public LoginResultDto()
    {
        AuthToken = new AuthTokenDto();
        Role = new RoleDto();
        RolePrivilege = new List<RolePrivilegeDto>();
    }
    public AuthTokenDto AuthToken { get; set; }
    public RoleDto Role { get; set; }
    public DepartmentDto Department { get; set; }
    public List<RolePrivilegeDto> RolePrivilege { get; set; }
}