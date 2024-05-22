
namespace LMS.Domain.Entities;
public class LeaveType : BaseEntity
{
    public string LeaveTypeName { get; set; }
    public int MaxLeaveCount { get; set; }
    public string? Description { get; set; }
}
