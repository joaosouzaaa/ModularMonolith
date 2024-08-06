using Microsoft.EntityFrameworkCore;

namespace ModularMonolith.Common.Settings.PaginationSettings;

public static class PaginationHandler
{
    public static async Task<PageList<TEntity>> PaginateAsync<TEntity>(
        this IQueryable<TEntity> query,
        PageParameters pageParameters,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var count = await query.CountAsync(cancellationToken);

        var entityPaginatedList = await query
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PageList<TEntity>(entityPaginatedList, count, pageParameters);
    }
}
