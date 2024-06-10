using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Entities;
public class UserLeave : BaseEntity
{
    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "LeaveTypeId is required.")]
    public int LeaveTypeId { get; set; }

    [Required(ErrorMessage = "FromDate is required.")]
    public DateTime FromDate { get; set; }

    [Required(ErrorMessage = "ToDate is required.")]
    public DateTime ToDate { get; set; }

    [StringLength(100, ErrorMessage = "Comments character length must not exceed 100 length.")]
    public string? Comments { get; set; }
    public int Status { get; set; } = 0;
    public virtual User User { get; set; }
    public virtual LeaveType? LeaveType { get; set; }
}
