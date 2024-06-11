namespace LMS.Application.Helpers.Pagination;

public class PageListConfig
{
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}