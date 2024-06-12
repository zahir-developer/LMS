namespace LMS.Application.Helpers.Pagination;

public class PagedListResult<T> where T : class
{
    public PagedListResult(List<T> items, PageListConfig pageListConfig, string searchText)
    {
        Items = items;
        PageListConfig = pageListConfig;
        SearchText = searchText;     
    }

    public PageListConfig PageListConfig { get; set; } = new PageListConfig();

    public string SearchText { get; set;}

    public List<T> Items { get; set; }
}
