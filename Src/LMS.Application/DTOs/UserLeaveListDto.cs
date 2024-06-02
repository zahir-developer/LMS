namespace LMS.Application.DTOs;
public class UserLeaveListDto : UserLeaveDto
{
    public string Name { get; set; } = string.Empty;
    public string? LeaveTypeName { get; set; } = string.Empty;
    public string? StatusName { get; set; } = string.Empty;
}
