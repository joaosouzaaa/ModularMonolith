using Microsoft.EntityFrameworkCore;

namespace ModularMonolith.Common.Settings.PaginationSettings;
public static class PaginationHandler
{
    public static async Task<PageList<TEntity>> PaginateAsync<TEntity>(IQueryable<TEntity> query, PageParameters pageParameters)
        where TEntity : class
    {
        var count = await query.CountAsync();
        var entityPaginatedList = await query.Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize).Take(pageParameters.PageSize).AsNoTracking().ToListAsync();

        return new PageList<TEntity>(entityPaginatedList, count, pageParameters);
    }
}
