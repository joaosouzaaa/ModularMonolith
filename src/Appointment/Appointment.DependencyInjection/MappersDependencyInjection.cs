using Appointment.ApplicationService.Mappers;
using Appointment.Domain.Interfaces.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IAppointmentTimeMapper, AppointmentTimeMapper>();
}
