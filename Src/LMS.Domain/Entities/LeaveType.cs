
namespace LMS.Domain.Entities;
public class LeaveType : BaseEntity
{
    public string LeaveTypeName { get; set; } = string.Empty;
    public int MaxLeaveCount { get; set; }
    public string? Description { get; set; }
}
