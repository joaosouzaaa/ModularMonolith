using Microsoft.Extensions.DependencyInjection;
using Patient.ApplicationServices.Services;
using Patient.Domain.Interfaces.Services;

namespace Patient.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IPatientClientService, PatientClientService>();
    }
}
