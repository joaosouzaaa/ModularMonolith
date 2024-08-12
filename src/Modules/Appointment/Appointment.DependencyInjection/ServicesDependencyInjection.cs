using Appointment.ApplicationService.Services;
using Appointment.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IAppointmentTimeService, AppointmentTimeService>();
}
