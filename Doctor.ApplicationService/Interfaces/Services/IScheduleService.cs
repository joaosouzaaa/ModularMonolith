using Doctor.Domain.Contracts;

namespace Doctor.ApplicationService.Interfaces.Services;
public interface IScheduleService
{
    Task AddAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent);
}
