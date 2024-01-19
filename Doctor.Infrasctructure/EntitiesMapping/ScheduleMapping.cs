using Doctor.Domain.Entities;
using Doctor.Infrasctructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrasctructure.EntitiesMapping;
public sealed class ScheduleMapping : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules", SchemaConstants.DoctorSchema);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Time)
            .IsRequired(true)
            .HasColumnName("time")
            .HasColumnType("timestamp with time zone");
    }
}
