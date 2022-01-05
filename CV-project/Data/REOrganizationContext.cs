using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CV_project.Data.Entities;

#nullable disable

namespace CV_project.Data
{
    public partial class REOrganizationContext : DbContext
    {
        public REOrganizationContext()
        {
        }

        public REOrganizationContext(DbContextOptions<REOrganizationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Apply> Applies { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<HaveEvent> HaveEvents { get; set; }
        public virtual DbSet<HaveJob> HaveJobs { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<WebCv> WebCvs { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("workstation id=REOrganization.mssql.somee.com;packet size=4096;user id=hoangvu130301_SQLLogin_2;pwd=b9mg6sx9u6;data source=REOrganization.mssql.somee.com;persist security info=False;initial catalog=REOrganization");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);
            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.Property(e => e.EmailAddress).IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Applicant_Account_FK");

                entity.HasOne(d => d.Cv)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.Cvid)
                    .HasConstraintName("Applicant_WebCV_FK");
            });

            modelBuilder.Entity<Apply>(entity =>
            {
                entity.HasKey(e => new { e.ApplicantId, e.CompanyId })
                    .HasName("Apply_PK");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Apply_Applicant_FK");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Apply_Company_FK");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Company_Account_FK");
            });

            modelBuilder.Entity<HaveEvent>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.EventId })
                    .HasName("HaveEvent_PK");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HaveEvents)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HaveEvent_Company_FK");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.HaveEvents)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HaveEvent_Event_FK");
            });

            modelBuilder.Entity<HaveJob>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.JobId })
                    .HasName("HaveJob_PK");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HaveJobs)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HaveJob_Company_FK");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.HaveJobs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HaveJob_Job_FK");
            });

            modelBuilder.Entity<WebCv>(entity =>
            {
                entity.HasKey(e => e.Cvid)
                    .HasName("WebCV_PK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
