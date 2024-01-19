using Doctor.Domain.Entities;
using Doctor.Infrasctructure.DatabaseContexts;
using Doctor.Infrasctructure.Interfaces.Repositories;
using Doctor.Infrasctructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

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
}
