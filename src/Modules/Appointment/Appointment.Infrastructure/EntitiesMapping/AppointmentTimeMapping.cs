using Appointment.Domain.Constants;
using Appointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Infrastructure.EntitiesMapping;

internal sealed class AppointmentTimeMapping : IEntityTypeConfiguration<AppointmentTime>
{
    public void Configure(EntityTypeBuilder<AppointmentTime> builder)
    {
        builder.ToTable("AppointmentsTime", SchemaConstants.AppointmentSchema);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Time)
            .IsRequired(true)
            .HasColumnName("time")
            .HasColumnType("timestamp without time zone");

        builder.Property(s => s.DoctorAttendantId)
            .IsRequired(true)
            .HasColumnName("doctor_attendant_id")
            .HasColumnType("integer");

        builder.Property(s => s.PatientClientId)
            .IsRequired(true)
            .HasColumnName("patient_client_id")
            .HasColumnType("integer");
    }
}
