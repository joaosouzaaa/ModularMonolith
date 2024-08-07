using Doctor.Domain.Constants;
using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.EntitiesMapping;

internal sealed class ScheduleMapping : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules", SchemaConstants.DoctorSchema);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Time)
            .IsRequired(true)
            .HasColumnName("time")
            .HasColumnType("timestamp without time zone");

        builder.HasOne(s => s.Doctor)
            .WithMany(d => d.Schedules)
            .HasForeignKey(s => s.DoctorAttendantId)
            .HasConstraintName("FK_DoctorAttendant_Schedule")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
