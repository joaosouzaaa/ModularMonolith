using Doctor.Infrasctructure.DatabaseContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Doctor.DependencyInjection;
public static class MigrationHandler
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<DoctorDbContext>();

        try
        {
            appContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
