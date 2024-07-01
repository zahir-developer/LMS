namespace LMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Holiday : BaseEntity
{
    [Required(ErrorMessage = "Leave Name is required.")]
    public string HolidayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Region is required.")]
    public string Region { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Description can't exceed length 200 characters.")]
    public string Description { get; set; } = string.Empty;
}