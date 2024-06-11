namespace LMS.Application.Helpers.Pagination;

public class PagedListResult<T> where T : class
{
    public PagedListResult(List<T> items, int pageNumber, int pageSize, int totalItems, int toalPages, string searchText)
    {
        Items = items;
        PageListConfig.PageNumber = pageNumber;
        PageListConfig.PageSize = pageSize;
        PageListConfig.TotalItems = totalItems;
        PageListConfig.TotalPages = toalPages;
        SearchText = searchText;     
    }

    public PageListConfig PageListConfig { get; set; } = new PageListConfig();

    public string SearchText { get; set;}

    public List<T> Items { get; set; }
}
