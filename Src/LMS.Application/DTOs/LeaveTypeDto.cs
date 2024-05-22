 
namespace LMS.Application.DTOs;
public class LeaveTypeDto
{
    public int LeaveTypeId { get; set; }
    public string LeaveName { get; set; }
    public int MaxLeaveCount { get; set; }
    public string? Description { get; set; }
}
