using Doctor.Infrasctructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrasctructure.Repositories.BaseRepositories;
public abstract class BaseRepository<TEntity>(DoctorDbContext dbContext) : IDisposable
    where TEntity : class
{
    private readonly DoctorDbContext _dbContext = dbContext;
    protected DbSet<TEntity> DbContextSet => _dbContext.Set<TEntity>();

    public void Dispose()
    {
        _dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    protected async Task<bool> SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync() > 0;
}
