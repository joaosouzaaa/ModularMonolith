using Doctor.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories.BaseRepositories;

public abstract class BaseRepository<TEntity> : IDisposable
    where TEntity : class
{
    protected readonly DbContext _dbContext;
    protected DbSet<TEntity> DbContextSet => _dbContext.Set<TEntity>();

    public BaseRepository(DoctorDbContext dbContext) =>
        _dbContext = dbContext;

    public void Dispose()
    {
        _dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    protected async Task<bool> SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync() > 0;
}
