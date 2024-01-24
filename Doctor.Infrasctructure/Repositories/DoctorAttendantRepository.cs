using Doctor.Domain.Arguments;
using Doctor.Domain.Entities;
using Doctor.Infrasctructure.DatabaseContexts;
using Doctor.Infrasctructure.Interfaces.Repositories;
using Doctor.Infrasctructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Infrasctructure.Repositories;
public sealed class DoctorAttendantRepository(DoctorDbContext dbContext) : BaseRepository<DoctorAttendant>(dbContext), IDoctorAttendantRepository
{
    public async Task<bool> AddAsync(DoctorAttendant doctorAttendant)
    {
        await DbContextSet.AddAsync(doctorAttendant);

        return await SaveChangesAsync();
    }

    public Task<bool> UpdateAsync(DoctorAttendant doctorAttendant)
    {
        _dbContext.Entry(doctorAttendant.Certification).State = EntityState.Modified;
        _dbContext.Entry(doctorAttendant).State = EntityState.Modified;

        return SaveChangesAsync();
    }

    public Task<PageList<DoctorAttendant>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterArgument filter)
    {
        var query = DbContextSet.Include(d => d.Specialities)
                                .Include(d => d.Schedules)
                                .Where(d => d.Specialities.Any(s => filter.SpecialityIds.Contains(s.Id)))
                                .Where(d => filter.InitialTime == null
                                || d.Schedules.Any(s => s.Time >= filter.InitialTime))
                                .Where(d => filter.FinalTime == null
                                || d.Schedules.Any(s => s.Time <= filter.FinalTime));

        return query.PaginateAsync(filter);
    }

    public Task<DoctorAttendant?> GetByIdAsync(int id) =>
        DbContextSet.AsNoTracking()
                    .Include(d => d.Certification)
                    .Include(d => d.Specialities)
                    .Include(d => d.Schedules)
                    .FirstOrDefaultAsync(d => d.Id == id);
}
