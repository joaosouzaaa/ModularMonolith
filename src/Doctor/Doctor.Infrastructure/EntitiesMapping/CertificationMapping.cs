using Doctor.Domain.Constants;
using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.EntitiesMapping;

internal sealed class CertificationMapping : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.ToTable("Certifications", SchemaConstants.DoctorSchema);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.LicenseNumber)
            .IsRequired(true)
            .HasColumnName("license_number")
            .HasColumnType("varchar(20)");

        builder.HasOne(c => c.DoctorAttendant)
           .WithOne(d => d.Certification)
           .HasForeignKey<DoctorAttendant>(d => d.CertificationId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
