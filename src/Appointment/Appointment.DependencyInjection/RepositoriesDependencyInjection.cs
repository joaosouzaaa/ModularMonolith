using Appointment.Domain.Interfaces.Repositories;
using Appointment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.DependencyInjection;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IAppointmentTimeRepository, AppointmentTimeRepository>();
}
