using Doctor.ApplicationService.DataTransferObjects.Schedule;
using Doctor.Domain.Contracts;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Interfaces.Mappers;
public interface IScheduleMapper
{
    List<ScheduleResponse> DomainListToResponseList(List<Schedule> scheduleList);
    Schedule AppointmentTimeCreatedEventToDomain(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent);
}
