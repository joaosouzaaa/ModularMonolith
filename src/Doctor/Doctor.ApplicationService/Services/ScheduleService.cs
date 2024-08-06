using Doctor.Domain.Contracts;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Domain.Interfaces.Services;

namespace Doctor.ApplicationService.Services;

public sealed class ScheduleService(
    IScheduleRepository scheduleRepository, 
    IScheduleMapper scheduleMapper) 
    : IScheduleService
{
    public async Task AddAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent)
    {
        var schedule = scheduleMapper.AppointmentTimeCreatedEventToDomain(appointmentTimeCreatedEvent);

        await scheduleRepository.AddAsync(schedule);
    }
}
