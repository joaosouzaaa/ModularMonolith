using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure.DatabaseContexts;

namespace Patient.DependencyInjection;
public static class MigrationHandler
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var patientDbContext = scope.ServiceProvider.GetRequiredService<PatientDbContext>();

        try
        {
            patientDbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
