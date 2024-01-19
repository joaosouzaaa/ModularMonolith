using Doctor.Domain.Entities;
using Doctor.Infrasctructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrasctructure.EntitiesMapping;
public sealed class CertificationMapping : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.ToTable("Certifications", SchemaConstants.DoctorSchema);
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.LicenseNunber)
            .IsRequired(true)
            .HasColumnName("license_number")
            .HasColumnType("varchar(20)");
    }
}
