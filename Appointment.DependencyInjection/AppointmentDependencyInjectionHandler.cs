using Appointment.Infrastructure.DatabaseContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Common.Factories;

namespace Appointment.DependencyInjection;
public static class AppointmentDependencyInjectionHandler
{
    public static void AddAppointmentDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppointmentDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString());
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddPublishersDependencyInjection();
        services.AddRepositoriesDependencyInjection();
        services.AddValidatorsDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
    }

    public static void UseAppointmentDependencyInjection(this IApplicationBuilder app)
    {
        app.MigrateDatabase();
    }
}
