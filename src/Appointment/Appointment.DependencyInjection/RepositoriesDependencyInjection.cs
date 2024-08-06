using Appointment.Domain.Interfaces.Repositories;
using Appointment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IAppointmentTimeRepository, AppointmentTimeRepository>();
}
