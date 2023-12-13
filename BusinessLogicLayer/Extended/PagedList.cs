namespace BusinessLogicLayer.Extended;
public class PagedList<T>(List<T> items, int count, int pageIndex, int pageSize)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int TotalCount { get; set; } = count;
    public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
    public List<T> Items { get; set; } = items;
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PagedList<T> ToPagedList(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageIndex, pageSize);
    }
}
