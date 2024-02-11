using Patient.Domain.Contracts;

namespace Patient.ApplicationServices.Intefaces.Services;
public interface IEmailService
{
    Task SendAppointmentEmailAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent);
}
