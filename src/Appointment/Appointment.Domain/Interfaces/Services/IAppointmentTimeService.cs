using Appointment.Domain.DataTransferObjects.Appointment;

namespace Appointment.Domain.Interfaces.Services;

public interface IAppointmentTimeService
{
    Task<bool> AddAsync(AppointmentTimeSave appointmentTimeSave);
}
