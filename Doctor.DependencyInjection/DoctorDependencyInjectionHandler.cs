using Doctor.Infrasctructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class DoctorDependencyInjectionHandler
{
    public static void AddDoctorDependencyInjectionHandler(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DoctorDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("LocalConnection"));
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddRepositoriesDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
