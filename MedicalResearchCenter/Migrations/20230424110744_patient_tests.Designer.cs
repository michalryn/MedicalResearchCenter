﻿// <auto-generated />
using System;
using MedicalResearchCenter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230424110744_patient_tests")]
    partial class patient_tests
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.LabReferral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Consent")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("ResearchProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("ResearchProjectId");

                    b.ToTable("LabReferrals");
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.LabTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("NormFrom")
                        .HasColumnType("float");

                    b.Property<double>("NormTo")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("LabTests");
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.PatientTest", b =>
                {
                    b.Property<int>("LabReferralId")
                        .HasColumnType("int");

                    b.Property<int>("LabTestId")
                        .HasColumnType("int");

                    b.Property<double>("Result")
                        .HasColumnType("float");

                    b.HasKey("LabReferralId", "LabTestId");

                    b.HasIndex("LabTestId");

                    b.ToTable("PatientTests");
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.ResearchProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manager")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("ResearchProjects");
                });

            modelBuilder.Entity("PatientResearchProject", b =>
                {
                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.Property<int>("ResearchProjectsId")
                        .HasColumnType("int");

                    b.HasKey("PatientsId", "ResearchProjectsId");

                    b.HasIndex("ResearchProjectsId");

                    b.ToTable("PatientResearchProject");
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.LabReferral", b =>
                {
                    b.HasOne("MedicalResearchCenter.Data.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalResearchCenter.Data.Entities.ResearchProject", "ReserachProject")
                        .WithMany()
                        .HasForeignKey("ResearchProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("ReserachProject");
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.PatientTest", b =>
                {
                    b.HasOne("MedicalResearchCenter.Data.Entities.LabReferral", "LabReferral")
                        .WithMany("PatientTests")
                        .HasForeignKey("LabReferralId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalResearchCenter.Data.Entities.LabTest", "LabTest")
                        .WithMany()
                        .HasForeignKey("LabTestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LabReferral");

                    b.Navigation("LabTest");
                });

            modelBuilder.Entity("PatientResearchProject", b =>
                {
                    b.HasOne("MedicalResearchCenter.Data.Entities.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicalResearchCenter.Data.Entities.ResearchProject", null)
                        .WithMany()
                        .HasForeignKey("ResearchProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedicalResearchCenter.Data.Entities.LabReferral", b =>
                {
                    b.Navigation("PatientTests");
                });
#pragma warning restore 612, 618
        }
    }
}
