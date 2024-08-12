using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patient.Domain.Constants;
using Patient.Domain.Entities;

namespace Patient.Infrastructure.EntitiesMapping;

internal sealed class PatientClientMapping : IEntityTypeConfiguration<PatientClient>
{
    public void Configure(EntityTypeBuilder<PatientClient> builder)
    {
        builder.ToTable("Patients", SchemaConstants.PatientSchema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Address)
            .IsRequired(true)
            .HasColumnName("address")
            .HasColumnType("varchar(200)");
    }
}
