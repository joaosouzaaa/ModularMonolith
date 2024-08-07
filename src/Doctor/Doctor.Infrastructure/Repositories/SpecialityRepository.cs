using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Infrastructure.DatabaseContexts;
using Doctor.Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public sealed class SpecialityRepository(
    DoctorDbContext dbContext)
    : BaseRepository<Speciality>(dbContext),
    ISpecialityRepository
{
    public async Task<bool> AddAsync(Speciality speciality, CancellationToken cancellationToken)
    {
        await DbContextSet.AddAsync(speciality, cancellationToken);

        return await SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var speciality = await DbContextSet.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        DbContextSet.Remove(speciality!);

        return await SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
        DbContextSet.AsNoTracking().AnyAsync(s => s.Id == id, cancellationToken);

    public Task<List<Speciality>> GetAllAsync(CancellationToken cancellationToken) =>
        DbContextSet.AsNoTracking().ToListAsync(cancellationToken);

    public Task<List<Speciality>> GetAllAsync(List<int> idList, CancellationToken cancellationToken) =>
        DbContextSet.Where(s => idList.Contains(s.Id)).ToListAsync(cancellationToken);
}
