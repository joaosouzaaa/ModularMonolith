﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patient.Domain.Entities;
using Patient.Infrastructure.Constants;

namespace Patient.Infrastructure.EntitiesMapping;
public sealed class PatientClientMapping : IEntityTypeConfiguration<PatientClient>
{
    public void Configure(EntityTypeBuilder<PatientClient> builder)
    {
        builder.ToTable("Patients", SchemaConstants.PatientSchema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");
    }
}