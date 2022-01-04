﻿// <auto-generated />
using System;
using CV_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CV_project.Migrations
{
    [DbContext(typeof(REOrganizationContext))]
    partial class REOrganizationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CV_project.Data.Entities.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("AccountId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Applicant", b =>
                {
                    b.Property<Guid>("ApplicantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ApplicantID");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("Activities")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Awards")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("BriefIntroduction")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CareerObjectives")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Certificates")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Cvid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CVID");

                    b.Property<string>("Cvupload")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CVUpload");

                    b.Property<DateTime?>("DoB")
                        .HasColumnType("date");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Major")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Skills")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("University")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<short?>("WorkExperiences")
                        .HasColumnType("smallint");

                    b.HasKey("ApplicantId");

                    b.HasIndex("AccountId");

                    b.HasIndex("Cvid");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Apply", b =>
                {
                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ApplicantID");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyID");

                    b.HasKey("ApplicantId", "CompanyId")
                        .HasName("Apply_PK");

                    b.HasIndex("CompanyId");

                    b.ToTable("Apply");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyID");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountID");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<short?>("NoEmployee")
                        .HasColumnType("smallint");

                    b.HasKey("CompanyId");

                    b.HasIndex("AccountId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("EventId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("CV_project.Data.Entities.HaveEvent", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyID");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EventID");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("EventEndDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("EventEndTime")
                        .HasColumnType("time(5)");

                    b.Property<string>("EventLink")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("EventStartDate")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("EventStartTime")
                        .HasColumnType("time(5)");

                    b.Property<string>("Venue")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CompanyId", "EventId")
                        .HasName("HaveEvent_PK");

                    b.HasIndex("EventId");

                    b.ToTable("HaveEvent");
                });

            modelBuilder.Entity("CV_project.Data.Entities.HaveJob", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyID");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("JobID");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumEmployee")
                        .HasColumnType("int");

                    b.Property<string>("Qualification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SalaryPerMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Worktime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId", "JobId")
                        .HasName("HaveJob_PK");

                    b.HasIndex("JobId");

                    b.ToTable("HaveJob");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Job", b =>
                {
                    b.Property<Guid>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("JobID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Vacancy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("JobId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("CV_project.Data.Entities.WebCv", b =>
                {
                    b.Property<Guid>("Cvid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CVID");

                    b.Property<string>("Activities")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Awards")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("BriefIntroduction")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CareerObjectives")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Certificates")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime?>("DoB")
                        .HasColumnType("date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Major")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Skills")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("University")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<short?>("WorkExperience")
                        .HasColumnType("smallint");

                    b.HasKey("Cvid")
                        .HasName("WebCV_PK");

                    b.ToTable("WebCV");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Applicant", b =>
                {
                    b.HasOne("CV_project.Data.Entities.Account", "Account")
                        .WithMany("Applicants")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("Applicant_Account_FK")
                        .IsRequired();

                    b.HasOne("CV_project.Data.Entities.WebCv", "Cv")
                        .WithMany("Applicants")
                        .HasForeignKey("Cvid")
                        .HasConstraintName("Applicant_WebCV_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Apply", b =>
                {
                    b.HasOne("CV_project.Data.Entities.Applicant", "Applicant")
                        .WithMany("Applies")
                        .HasForeignKey("ApplicantId")
                        .HasConstraintName("Apply_Applicant_FK")
                        .IsRequired();

                    b.HasOne("CV_project.Data.Entities.Company", "Company")
                        .WithMany("Applies")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("Apply_Company_FK")
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Company", b =>
                {
                    b.HasOne("CV_project.Data.Entities.Account", "Account")
                        .WithMany("Companies")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("Company_Account_FK")
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CV_project.Data.Entities.HaveEvent", b =>
                {
                    b.HasOne("CV_project.Data.Entities.Company", "Company")
                        .WithMany("HaveEvents")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("HaveEvent_Company_FK")
                        .IsRequired();

                    b.HasOne("CV_project.Data.Entities.Event", "Event")
                        .WithMany("HaveEvents")
                        .HasForeignKey("EventId")
                        .HasConstraintName("HaveEvent_Event_FK")
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("CV_project.Data.Entities.HaveJob", b =>
                {
                    b.HasOne("CV_project.Data.Entities.Company", "Company")
                        .WithMany("HaveJobs")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("HaveJob_Company_FK")
                        .IsRequired();

                    b.HasOne("CV_project.Data.Entities.Job", "Job")
                        .WithMany("HaveJobs")
                        .HasForeignKey("JobId")
                        .HasConstraintName("HaveJob_Job_FK")
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Account", b =>
                {
                    b.Navigation("Applicants");

                    b.Navigation("Companies");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Applicant", b =>
                {
                    b.Navigation("Applies");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Company", b =>
                {
                    b.Navigation("Applies");

                    b.Navigation("HaveEvents");

                    b.Navigation("HaveJobs");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Event", b =>
                {
                    b.Navigation("HaveEvents");
                });

            modelBuilder.Entity("CV_project.Data.Entities.Job", b =>
                {
                    b.Navigation("HaveJobs");
                });

            modelBuilder.Entity("CV_project.Data.Entities.WebCv", b =>
                {
                    b.Navigation("Applicants");
                });
#pragma warning restore 612, 618
        }
    }
}
