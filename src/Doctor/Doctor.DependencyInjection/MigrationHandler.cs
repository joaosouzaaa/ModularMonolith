using Doctor.Infrastructure.DatabaseContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;

internal static class MigrationHandler
{
    internal static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var doctorDbContext = scope.ServiceProvider.GetRequiredService<DoctorDbContext>();

        try
        {
            doctorDbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
