namespace ModularMonolith.Common.Settings.PaginationSettings;

public class PageList<TEntity>
    where TEntity : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<TEntity> Result { get; set; } = [];

    public PageList() { }

    public PageList(List<TEntity> items, int count, PageParameters pageParameters)
    {
        Result = items;
        TotalCount = count;
        PageSize = pageParameters.PageSize;
        CurrentPage = pageParameters.PageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageParameters.PageSize);
    }
}
