using Doctor.Domain.Arguments;
using Doctor.Domain.Entities;
using Doctor.Infrasctructure.DatabaseContexts;
using Doctor.Infrasctructure.Interfaces.Repositories;
using Doctor.Infrasctructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Infrasctructure.Repositories;
public sealed class DoctorAttendantRepository : BaseRepository<DoctorAttendant>, IDoctorAttendantRepository
{
    public DoctorAttendantRepository(DoctorDbContext dbContext) : base(dbContext)
    {
        
    }

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

    public Task<DoctorAttendant?> GetByIdAsync(int id, bool asNoTracking)
    {
        var query = (IQueryable<DoctorAttendant>)DbContextSet;

        if (asNoTracking)
            query = DbContextSet.AsNoTracking();

        return DbContextSet.Include(d => d.Certification)
                    .Include(d => d.Specialities)
                    .Include(d => d.Schedules)
                    .FirstOrDefaultAsync(d => d.Id == id);
    }
}
