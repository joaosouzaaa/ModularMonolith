using Appointment.DependencyInjection;
using Doctor.DependencyInjection;
using Patient.DependencyInjection;

namespace ModularMonolith.API.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();

        services.AddSettingsDependencyInjection();
        services.AddOptionsDependencyInjection(configuration);

        services.AddAppointmentDependencyInjection(configuration);
        services.AddDoctorDependencyInjection(configuration);
        services.AddPatientDependencyInjection(configuration);
    }

    internal static void UseDependencyInjection(this IApplicationBuilder app)
    {
        app.UseDoctorDependencyInjection();
        app.UsePatientDependencyInjection();
        app.UseAppointmentDependencyInjection();
    }
}
