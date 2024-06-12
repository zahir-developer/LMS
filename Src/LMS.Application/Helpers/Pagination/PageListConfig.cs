namespace LMS.Application.Helpers.Pagination;

public class PageListConfig
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public string SortBy { get; set; }
    public string SortDir { get; set; }
}