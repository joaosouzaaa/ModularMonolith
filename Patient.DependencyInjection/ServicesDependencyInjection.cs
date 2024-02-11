using Microsoft.Extensions.DependencyInjection;
using Patient.ApplicationServices.Intefaces.Services;
using Patient.ApplicationServices.Services;

namespace Patient.DependencyInjection;
public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPatientClientService, PatientClientService>();
    }
}
