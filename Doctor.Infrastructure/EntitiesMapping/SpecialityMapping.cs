using Doctor.Domain.Constants;
using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrasctructure.EntitiesMapping;
public sealed class SpecialityMapping : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.ToTable("Specialities", SchemaConstants.DoctorSchema);

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");
    }
}
