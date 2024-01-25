using Doctor.DependencyInjection;
using Patient.DependencyInjection;

namespace ModularMonolith.API.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDoctorDependencyInjection(configuration);
        services.AddPatientDependencyInjection(configuration);
    }
}
