using Doctor.Domain.Contracts;
using Doctor.Domain.DataTransferObjects.Schedule;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;

namespace Doctor.ApplicationService.Mappers;

public sealed class ScheduleMapper : IScheduleMapper
{
    public Schedule AppointmentTimeCreatedEventToDomain(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent) =>
        new()
        {
            DoctorAttendantId = appointmentTimeCreatedEvent.DoctorAttendantId,
            Time = appointmentTimeCreatedEvent.Time
        };

    public List<ScheduleResponse> DomainListToResponseList(List<Schedule> scheduleList) =>
        scheduleList.Select(DomainToResponse).ToList();

    private static ScheduleResponse DomainToResponse(Schedule schedule) =>
        new(schedule.Id,
            schedule.Time);
}
