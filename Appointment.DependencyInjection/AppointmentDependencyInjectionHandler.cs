using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;
public static class AppointmentDependencyInjectionHandler
{
    public static void AddAppointmentDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsDependencyInjection(configuration);
        services.AddPublishersDependencyInjection();
        services.AddRepositoriesDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
