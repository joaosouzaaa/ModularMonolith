using Doctor.Domain.Entities;
using Doctor.Infrasctructure.DatabaseContexts;
using Doctor.Infrasctructure.Interfaces.Repositories;
using Doctor.Infrasctructure.Repositories.BaseRepositories;

namespace Doctor.Infrasctructure.Repositories;
public sealed class ScheduleRepository(DoctorDbContext dbContext) : BaseRepository<Schedule>(dbContext), IScheduleRepository
{
}
