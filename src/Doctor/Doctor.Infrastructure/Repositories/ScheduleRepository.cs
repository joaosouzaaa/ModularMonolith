using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Infrastructure.DatabaseContexts;
using Doctor.Infrastructure.Repositories.BaseRepositories;

namespace Doctor.Infrastructure.Repositories;

public sealed class ScheduleRepository(
    DoctorDbContext dbContext) 
    : BaseRepository<Schedule>(dbContext), 
    IScheduleRepository
{
    public async Task<bool> AddAsync(Schedule schedule)
    {
        await DbContextSet.AddAsync(schedule);

        return await SaveChangesAsync();
    }
}
