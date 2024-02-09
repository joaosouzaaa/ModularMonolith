using Appointment.Domain.Contracts;

namespace Appointment.Infrastructure.Interfaces.Publishers;
public interface IAppointmentPublisher
{
    void PublishAppointmentTimeCreatedMessage(AppointmentTimeCreatedEvent appointment);
}
