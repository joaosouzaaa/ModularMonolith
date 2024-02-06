using Doctor.ApplicationService.Interfaces.Services;
using Doctor.ApplicationService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ISpecialityService, SpecialityService>();
    }
}
