using Appointment.ApplicationService.Mappers;
using Appointment.Domain.Interfaces.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;

public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IAppointmentTimeMapper, AppointmentTimeMapper>();
}
