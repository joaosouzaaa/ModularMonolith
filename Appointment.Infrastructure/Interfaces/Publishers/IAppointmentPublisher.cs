using Appointment.Domain.Contracts;

namespace Appointment.Infrastructure.Interfaces.Publishers;
public interface IAppointmentPublisher
{
    void PublishAppointmentCreatedMessage(AppointmentCreatedEvent appointment);
}
