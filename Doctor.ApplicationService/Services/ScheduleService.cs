using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Interfaces.Services;
using Doctor.Domain.Contracts;
using Doctor.Infrasctructure.Interfaces.Repositories;

namespace Doctor.ApplicationService.Services;
public sealed class ScheduleService(IScheduleRepository scheduleRepository, IScheduleMapper scheduleMapper) : IScheduleService
{
    public async Task AddAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent)
    {
        var schedule = scheduleMapper.AppointmentTimeCreatedEventToDomain(appointmentTimeCreatedEvent);

        await scheduleRepository.AddAsync(schedule);
    }
}
