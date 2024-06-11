namespace LMS.Application.Helpers.Pagination;

public class PagedListResult<T> where T : class
{
    public PagedListResult(List<T> items, int currentPage, int itemsPerPage, int totalItems, int toalPages)
    {
        Items = items;
        PageListConfig.CurrentPage = currentPage;
        PageListConfig.ItemsPerPage = itemsPerPage;
        PageListConfig.TotalItems = totalItems;
        PageListConfig.TotalPages = toalPages;       
    }

    public PageListConfig PageListConfig { get; set; } = new PageListConfig();

    public List<T> Items { get; set; }
}
