using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure.DatabaseContexts;

namespace Patient.DependencyInjection;
public static class PatientDependencyInjection
{
    public static void AddPatientDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PatientDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("LocalConnection"));
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });
    }
}
