using Doctor.DependencyInjection;
using Patient.DependencyInjection;

namespace ModularMonolith.API.DependencyInjection;

public static class DependencyInjectionHandler
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();

        services.AddSettingsDependencyInjection();
        services.AddFilterDependencyInjection();

        services.AddDoctorDependencyInjection(configuration);
        services.AddPatientDependencyInjection(configuration);
    }

    public static void UseDependencyInjection(this IApplicationBuilder app)
    {
        app.UseDoctorDependencyInjection();
        app.UsePatientDependencyInjection();
    }
}
