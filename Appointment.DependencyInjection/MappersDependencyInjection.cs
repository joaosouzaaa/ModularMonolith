using Appointment.ApplicationService.Interfaces.Mappers;
using Appointment.ApplicationService.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;
public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentTimeMapper, AppointmentTimeMapper>();
    }
}
