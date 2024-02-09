using Appointment.ApplicationService.Interfaces.Services;
using Appointment.ApplicationService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;
public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentTimeService, AppointmentTimeService>();
    }
}
