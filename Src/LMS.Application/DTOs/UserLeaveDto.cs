namespace LMS.Application.DTOs;
public class UserLeaveDto
{
    public UserLeaveDto()
    {
        User = new UserDto();
        LeaveType = new LeaveTypeDto();
    }
    public int UserId { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
    public int Status { get; set; }
    public UserDto? User { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
}
