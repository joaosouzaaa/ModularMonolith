using Doctor.Domain.Interfaces.Repositories;
using Doctor.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDoctorAttendantRepository, DoctorAttendantRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ISpecialityRepository, SpecialityRepository>();
    }
}
