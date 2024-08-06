using Doctor.ApplicationService.Services;
using Doctor.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDoctorAttendantService, DoctorAttendantService>();
        services.AddScoped<IScheduleService, ScheduleService>();

        services.AddScoped<ISpecialityService, SpecialityService>();
        services.AddScoped<ISpecialityServiceFacade, SpecialityService>();
    }
}
