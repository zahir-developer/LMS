using System.ComponentModel.DataAnnotations;
using LMS.Domain.Entities;

namespace LMS.Domain.Entities;

public class Department : BaseEntity
{
    [Required(ErrorMessage = "Department name is required.")]
    [StringLength(50, ErrorMessage = "Department name character length must not exceed 50 length.")]
    public string DepartmentName { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Description character length must not exceed 100 length.")]    
    public string? Description { get; set;}
}
