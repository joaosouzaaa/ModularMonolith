using Appointment.Domain.Contracts;

namespace Appointment.Domain.Interfaces.Publishers;

public interface IAppointmentPublisher
{
    void PublishAppointmentTimeCreatedMessage(AppointmentTimeCreatedEvent appointment);
}
