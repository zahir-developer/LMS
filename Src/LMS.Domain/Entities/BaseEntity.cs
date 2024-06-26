using System.ComponentModel.DataAnnotations;
namespace LMS.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public virtual int Id { get; protected set;}
    public bool IsEnabled { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
}