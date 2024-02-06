using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure.Interfaces.Repositories;
using Patient.Infrastructure.Repositories;

namespace Patient.DependencyInjection;
public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPatientClientRepository, PatientClientRepository>();
    }
}
