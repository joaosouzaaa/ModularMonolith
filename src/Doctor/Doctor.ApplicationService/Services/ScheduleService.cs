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
    public Task AddAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent, CancellationToken cancellationToken)
    {
        var schedule = scheduleMapper.AppointmentTimeCreatedEventToDomain(appointmentTimeCreatedEvent);

        return scheduleRepository.AddAsync(schedule, cancellationToken);
    }
}
