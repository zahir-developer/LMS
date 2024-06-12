namespace LMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class LeaveType : BaseEntity
{

    [Required(ErrorMessage = "Leave Type name is required.")]
    [StringLength(50, ErrorMessage = "LeaveType character length must not exceed 50 length.")]
    public string LeaveTypeName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Max Leave Count is required.")]
    public int MaxLeaveCount { get; set; }

    [StringLength(100, ErrorMessage = "Description character length must not exceed 100 length.")]
    public string? Description { get; set; }
}
