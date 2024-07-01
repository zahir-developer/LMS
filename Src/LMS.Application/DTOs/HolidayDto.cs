namespace LMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class HolidayDto
{
    public int Id { get; set; }
    public string HolidayName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Region { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}