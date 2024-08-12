using Appointment.Domain.Interfaces.Publishers;
using Appointment.Infrastructure.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;

internal static class PublishersDependencyInjection
{
    internal static void AddPublishersDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IAppointmentPublisher, AppointmentPublisher>();
}
