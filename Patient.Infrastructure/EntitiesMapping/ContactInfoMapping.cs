using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patient.Domain.Entities;
using Patient.Infrastructure.Constants;

namespace Patient.Infrastructure.EntitiesMapping;
public sealed class ContactInfoMapping : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.ToTable("ContactInfos", SchemaConstants.PatientSchema);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.PhoneNumber)
            .IsRequired(true)
            .HasColumnName("phone_number")
            .HasColumnType("char(11)");

        builder.Property(c => c.Email)
            .IsRequired(true)
            .HasColumnName("email")
            .HasColumnType("varchar(100)");

        builder.HasOne(c => c.PatientClient)
            .WithOne(p => p.ContactInfo)
            .HasForeignKey<ContactInfo>(c => c.PatientClientId)
            .HasConstraintName("FK_ContactInfo_PatientClient")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
