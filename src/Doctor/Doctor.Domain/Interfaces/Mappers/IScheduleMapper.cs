using Doctor.Domain.Contracts;
using Doctor.Domain.DataTransferObjects.Schedule;
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Mappers;

public interface IScheduleMapper
{
    Schedule AppointmentTimeCreatedEventToDomain(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent);
    List<ScheduleResponse> DomainListToResponseList(List<Schedule> scheduleList);
}
