using Doctor.Domain.Entities;
using Doctor.Infrasctructure.DatabaseContexts;
using Doctor.Infrasctructure.Interfaces.Repositories;
using Doctor.Infrasctructure.Repositories.BaseRepositories;

namespace Doctor.Infrasctructure.Repositories;
public sealed class DoctorAttendantRepository(DoctorDbContext dbContext) : BaseRepository<DoctorAttendant>(dbContext), IDoctorAttendantRepository
{
    public async Task<bool> AddAsync(DoctorAttendant doctorAttendant)
    {
        await DbContextSet.AddAsync(doctorAttendant);

        return await SaveChangesAsync();
    }
}
