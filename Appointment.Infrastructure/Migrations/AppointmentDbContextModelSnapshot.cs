﻿// <auto-generated />
using System;
using Appointment.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Appointment.Infrastructure.Migrations
{
    [DbContext(typeof(AppointmentDbContext))]
    partial class AppointmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Appointment.Domain.Entities.AppointmentTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorAttendantId")
                        .HasColumnType("integer")
                        .HasColumnName("doctor_attendant_id");

                    b.Property<int>("PatientClientId")
                        .HasColumnType("integer")
                        .HasColumnName("patient_client_id");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("time");

                    b.HasKey("Id");

                    b.ToTable("AppointmentsTime", "Appointment");
                });
#pragma warning restore 612, 618
        }
    }
}
