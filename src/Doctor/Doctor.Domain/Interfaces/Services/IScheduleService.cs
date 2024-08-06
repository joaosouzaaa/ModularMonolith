using Doctor.Domain.Contracts;

namespace Doctor.Domain.Interfaces.Services;

public interface IScheduleService
{
    Task AddAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent);
}
