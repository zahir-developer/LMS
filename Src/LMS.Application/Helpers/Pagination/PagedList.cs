
using System.CodeDom;
using System.Data.Entity;

namespace LMS.Application.Helpers.Pagination;

public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        Items = items;
        AddRange(items);
    }

    public IEnumerable<T> Items { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();

        var skip = (pageNumber - 1) * pageSize;

        if(skip >= count)
        {
            skip = pageSize * 1;
        }

        var items = source.Skip(skip).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}