using Doctor.Domain.Entities;

namespace Doctor.Infrasctructure.Interfaces.Repositories;
public interface IScheduleRepository
{
    Task<bool> AddAsync(Schedule schedule);
}
