namespace ModularMonolith.Common.Settings.PaginationSettings;
public class PageParameters
{
    private int _pageSize = 1;
    public required int PageSize
    {
        get => _pageSize;
        set => _pageSize = value <= 0 ? _pageSize : value;
    }

    private int _pageNumber = 1;
    public required int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value <= 0 ? _pageNumber : value;
    }
}
