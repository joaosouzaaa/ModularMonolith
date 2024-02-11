using Microsoft.EntityFrameworkCore;
using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Doctor.Domain.Constants;

namespace Doctor.Infrasctructure.EntitiesMapping;
public sealed class DoctorAttendantMapping : IEntityTypeConfiguration<DoctorAttendant>
{
    public void Configure(EntityTypeBuilder<DoctorAttendant> builder)
    {
        builder.ToTable("Doctors", SchemaConstants.DoctorSchema);

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");

        builder.Property(d => d.ExperienceYears)
            .IsRequired(true)
            .HasColumnName("experience_years")
            .HasColumnType("integer");

        builder.Property(d => d.BirthDate)
            .IsRequired(true)
            .HasColumnName("birth_date")
            .HasColumnType("date");

        builder.HasOne(d => d.Certification)
            .WithOne()
            .HasForeignKey<DoctorAttendant>(d => d.CertificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Schedules)
            .WithOne(s => s.Doctor)
            .HasForeignKey(s => s.DoctorAttendantId)
            .HasConstraintName("FK_DoctorAttendant_Schedule")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Specialities)
            .WithMany(d => d.Doctors)
            .UsingEntity<Dictionary<string, object>>(options =>
            {
                options.HasOne<Speciality>().WithMany().HasForeignKey("SpecialitiesId");
                options.HasOne<DoctorAttendant>().WithMany().HasForeignKey("DoctorsId");
            });

    }
}
