using System.Linq.Expressions;
using Mapster;

namespace Application;

public class PaginatedList<T> : List<T>
{
    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public PaginatedList()
    {
    }

    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static Task<PaginatedList<T>> CreateAsync(List<T> source,int count, int pageIndex, int pageSize)
    {
        return Task.FromResult(new PaginatedList<T>(source, count, pageIndex, pageSize));
    }

    public static Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return Task.FromResult(new PaginatedList<T>(items, count, pageIndex, pageSize));
    }

    public static Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, Expression<Func<T, object>> orderBy,
        int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return Task.FromResult(new PaginatedList<T>(items, count, pageIndex, pageSize));
    }

    public static Task<PaginatedList<TD>> CreateAsync<TD>(IQueryable<T> source, Expression<Func<T, object>> orderBy,
        int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return Task.FromResult(new PaginatedList<TD>(items.AsQueryable().ProjectToType<TD>().ToList(), count, pageIndex,
            pageSize));
    }
}