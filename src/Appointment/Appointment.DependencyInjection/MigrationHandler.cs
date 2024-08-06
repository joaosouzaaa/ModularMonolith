﻿using Appointment.Infrastructure.DatabaseContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore; 

namespace Appointment.DependencyInjection;

internal static class MigrationHandler
{
    internal static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var appointmentDbContext = scope.ServiceProvider.GetRequiredService<AppointmentDbContext>();

        try
        {
            appointmentDbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
