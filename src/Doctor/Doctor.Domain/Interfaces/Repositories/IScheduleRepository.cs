using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task<bool> AddAsync(Schedule schedule);
}
