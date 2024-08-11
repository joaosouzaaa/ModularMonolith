using Doctor.Infrastructure.DatabaseContexts;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Common.Factories;

namespace Doctor.DependencyInjection;

public static class DoctorDependencyInjectionHandler
{
    public static void AddDoctorDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DoctorDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString());
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddRepositoriesDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
        services.AddConsumersDependencyInjection();

        services.AddValidatorsFromAssembly(ApplicationService.AssemblyReference.Assembly);
    }

    public static void UseDoctorDependencyInjection(this IApplicationBuilder app) =>
        app.MigrateDatabase();
}
