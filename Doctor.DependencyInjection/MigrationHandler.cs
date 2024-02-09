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
