using Doctor.Domain.Arguments;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Infrastructure.DatabaseContexts;
using Doctor.Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Infrastructure.Repositories;

public sealed class DoctorAttendantRepository(DoctorDbContext dbContext) : BaseRepository<DoctorAttendant>(dbContext), IDoctorAttendantRepository
{
    public async Task<bool> AddAsync(DoctorAttendant doctorAttendant, CancellationToken cancellationToken)
    {
        await DbContextSet.AddAsync(doctorAttendant, cancellationToken);

        return await SaveChangesAsync(cancellationToken);
    }

    public Task<PageList<DoctorAttendant>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterArgument filter, CancellationToken cancellationToken)
    {
        var query = DbContextSet
            .Include(d => d.Certification)
            .Include(d => d.Specialities)
            .Include(d => d.Schedules)
            .Where(d => !filter.SpecialityIds.Any() ||
            d.Specialities.Any(s => filter.SpecialityIds.Contains(s.Id)))
            .Where(d => filter.InitialTime == null ||
            d.Schedules.Any(s => s.Time >= filter.InitialTime))
            .Where(d => filter.FinalTime == null ||
            d.Schedules.Any(s => s.Time <= filter.FinalTime));

        return query.PaginateAsync(filter, cancellationToken);
    }

    public Task<DoctorAttendant?> GetByIdAsync(int id, bool asNoTracking, CancellationToken cancellationToken)
    {
        var query = (IQueryable<DoctorAttendant>)DbContextSet;

        if (asNoTracking)
        {
            query = DbContextSet.AsNoTracking();
        }

        return DbContextSet
            .Include(d => d.Certification)
            .Include(d => d.Specialities)
            .Include(d => d.Schedules)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public Task<bool> UpdateAsync(DoctorAttendant doctorAttendant, CancellationToken cancellationToken)
    {
        _dbContext.Entry(doctorAttendant.Certification).State = EntityState.Modified;
        _dbContext.Entry(doctorAttendant).State = EntityState.Modified;

        return SaveChangesAsync(cancellationToken);
    }
}
