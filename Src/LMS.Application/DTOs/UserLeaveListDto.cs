namespace LMS.Application.DTOs;
public class UserLeaveListDto
{
    public string Name { get; set; }
    public string LeaveTypeName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
    public string Status { get; set; }
}
