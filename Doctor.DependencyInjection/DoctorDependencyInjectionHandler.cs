using Doctor.Infrasctructure.DatabaseContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class DoctorDependencyInjectionHandler
{
    public static void AddDoctorDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DoctorDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("LocalConnection"));
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddRepositoriesDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddValidatorsDependencyInjection();
        services.AddServicesDependencyInjection();
    }

    public static void UseDoctorDependencyInjection(this IApplicationBuilder app)
    {
        app.MigrateDatabase();
    }
}
