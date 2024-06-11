namespace LMS.Application.Helpers.Pagination;

public class PagedListResult<T> where T : class
{
    public PagedListResult(List<T> items, int pageNumber, int pageSize, int totalItems, int toalPages)
    {
        Items = items;
        PageListConfig.PageNumber = pageNumber;
        PageListConfig.PageSize = pageSize;
        PageListConfig.TotalItems = totalItems;
        PageListConfig.TotalPages = toalPages;       
    }

    public PageListConfig PageListConfig { get; set; } = new PageListConfig();

    public List<T> Items { get; set; }
}
