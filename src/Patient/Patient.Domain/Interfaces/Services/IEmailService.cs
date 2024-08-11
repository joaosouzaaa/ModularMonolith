using Patient.Domain.Contracts;

namespace Patient.Domain.Interfaces.Services;

public interface IEmailService
{
    Task SendAppointmentEmailAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent, CancellationToken cancellationToken);
}
