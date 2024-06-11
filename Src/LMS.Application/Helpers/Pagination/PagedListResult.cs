namespace LMS.Application.Helpers.Pagination;

public class PagedListResult<T> where T : class
{
    public PagedListResult(List<T> items, int currentPage, int itemsPerPage, int totalItems, int toalPages)
    {
        Items = items;
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = toalPages;       
    }

    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; }
}
