using LMS.Domain.Entities;

namespace LMS.Domain.Entities;

public class Department : BaseEntity
{
    public string DepartmentName { get; set; } = string.Empty;
    public string? DepartmentDescription { get; set;}
}
