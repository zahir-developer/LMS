using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application.Helpers.Pagination;

public class UserParams
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public string SearchText { get; set; } = string.Empty;

    public string SortBy { get; set; } = string.Empty;
    
    public SortDirection SortDir { get; set; }
     
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

}