using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure.DatabaseContexts;
using ModularMonolith.Common.Factories;
using FluentValidation;

namespace Patient.DependencyInjection;

public static class PatientDependencyInjectionHandler
{
    public static void AddPatientDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PatientDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString());
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        services.AddRepositoriesDependencyInjection();
        services.AddEmailSettingsDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
        services.AddConsumersDependencyInjection();

        services.AddValidatorsFromAssembly(Domain.AssemblyReference.Assembly);
    }

    public static void UsePatientDependencyInjection(this IApplicationBuilder app) =>
        app.MigrateDatabase();
}
