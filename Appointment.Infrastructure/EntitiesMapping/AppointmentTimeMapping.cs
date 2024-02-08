using Appointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Infrastructure.EntitiesMapping;
public sealed class AppointmentTimeMapping : IEntityTypeConfiguration<AppointmentTime>
{
    public void Configure(EntityTypeBuilder<AppointmentTime> builder)
    {
        throw new NotImplementedException();
    }
}
