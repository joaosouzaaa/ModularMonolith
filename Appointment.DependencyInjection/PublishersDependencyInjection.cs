using Appointment.Infrastructure.Interfaces.Publishers;
using Appointment.Infrastructure.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;
public static class PublishersDependencyInjection
{
    public static void AddPublishersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentPublisher, AppointmentPublisher>();
    }
}
