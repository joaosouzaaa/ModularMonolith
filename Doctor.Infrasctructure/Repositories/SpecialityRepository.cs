using Doctor.Domain.Entities;
using Doctor.Infrasctructure.DatabaseContexts;
using Doctor.Infrasctructure.Interfaces.Repositories;
using Doctor.Infrasctructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrasctructure.Repositories;
public sealed class SpecialityRepository(DoctorDbContext dbContext) : BaseRepository<Speciality>(dbContext), ISpecialityRepository
{
    public async Task<bool> AddAsync(Speciality speciality)
    {
        await DbContextSet.AddAsync(speciality);

        return await SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        DbContextSet.AsNoTracking().AnyAsync(s => s.Id == id);

    public async Task<bool> DeleteAsync(int id)
    {
        var speciality = await DbContextSet.FirstOrDefaultAsync(s => s.Id == id);

        DbContextSet.Remove(speciality!);

        return await SaveChangesAsync();
    }

    public Task<List<Speciality>> GetAllAsync() =>
        DbContextSet.AsNoTracking().ToListAsync();
}
