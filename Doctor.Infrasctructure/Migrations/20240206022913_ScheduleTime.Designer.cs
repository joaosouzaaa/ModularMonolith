﻿// <auto-generated />
using System;
using Doctor.Infrasctructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Doctor.Infrasctructure.Migrations
{
    [DbContext(typeof(DoctorDbContext))]
    [Migration("20240206022913_ScheduleTime")]
    partial class ScheduleTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Doctor.Domain.Entities.Certification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("license_number");

                    b.HasKey("Id");

                    b.ToTable("Certifications", "Doctor");
                });

            modelBuilder.Entity("Doctor.Domain.Entities.DoctorAttendant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<int>("CertificationId")
                        .HasColumnType("integer");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("integer")
                        .HasColumnName("experience_years");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CertificationId")
                        .IsUnique();

                    b.ToTable("Doctors", "Doctor");
                });

            modelBuilder.Entity("Doctor.Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("time");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Schedules", "Doctor");
                });

            modelBuilder.Entity("Doctor.Domain.Entities.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Specialities", "Doctor");
                });

            modelBuilder.Entity("DoctorAttendantSpeciality", b =>
                {
                    b.Property<int>("DoctorsId")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialitiesId")
                        .HasColumnType("integer");

                    b.HasKey("DoctorsId", "SpecialitiesId");

                    b.HasIndex("SpecialitiesId");

                    b.ToTable("DoctorAttendantSpeciality", "Doctor");
                });

            modelBuilder.Entity("Doctor.Domain.Entities.DoctorAttendant", b =>
                {
                    b.HasOne("Doctor.Domain.Entities.Certification", "Certification")
                        .WithOne()
                        .HasForeignKey("Doctor.Domain.Entities.DoctorAttendant", "CertificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Certification");
                });

            modelBuilder.Entity("Doctor.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("Doctor.Domain.Entities.DoctorAttendant", "Doctor")
                        .WithMany("Schedules")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("DoctorAttendantSpeciality", b =>
                {
                    b.HasOne("Doctor.Domain.Entities.DoctorAttendant", null)
                        .WithMany()
                        .HasForeignKey("DoctorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doctor.Domain.Entities.Speciality", null)
                        .WithMany()
                        .HasForeignKey("SpecialitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Doctor.Domain.Entities.DoctorAttendant", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
