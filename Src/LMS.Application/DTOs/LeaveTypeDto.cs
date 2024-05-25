 
namespace LMS.Application.DTOs;
public class LeaveTypeDto
{
    public int Id { get; set;}
    public string LeaveTypeName { get; set; }
    public int MaxLeaveCount { get; set; }
    public string? Description { get; set; }
}
