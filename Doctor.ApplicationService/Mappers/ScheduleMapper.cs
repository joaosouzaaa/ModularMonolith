using Doctor.ApplicationService.DataTransferObjects.Schedule;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.Domain.Contracts;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Mappers;
public sealed class ScheduleMapper : IScheduleMapper
{
    public List<ScheduleResponse> DomainListToResponseList(List<Schedule> scheduleList) =>
        scheduleList.Select(DomainToResponse).ToList();

    public Schedule AppointmentTimeCreatedEventToDomain(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent) =>
        new()
        {
            DoctorAttendantId = appointmentTimeCreatedEvent.DoctorAttendantId,
            Time = appointmentTimeCreatedEvent.Time
        };

    private ScheduleResponse DomainToResponse(Schedule schedule) =>
        new()
        {
            Id = schedule.Id,
            Time = schedule.Time
        };
}
