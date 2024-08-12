using Microsoft.Extensions.DependencyInjection;
using Patient.Domain.Interfaces.Repositories;
using Patient.Infrastructure.Repositories;

namespace Patient.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPatientClientRepository, PatientClientRepository>();
        services.AddScoped<IPatientClientRepositoryFacade, PatientClientRepository>();
    }
}
