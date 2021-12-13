using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CV_project.Data.Entities;

#nullable disable

namespace CV_project.Data
{
    public partial class REdatabaseContext : DbContext
    {
        public REdatabaseContext()
        {
        }

        public REdatabaseContext(DbContextOptions<REdatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Apply> Applies { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<WebCv> WebCvs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=RE-database.mssql.somee.com;packet size=4096;user id=HuyDiep_SQLLogin_1;pwd=blmgmqunow;data source=RE-database.mssql.somee.com;persist security info=False;initial catalog=RE-database");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.Property(e => e.Account).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.Cv)
                    .WithMany(p => p.Applicants)
                    .HasForeignKey(d => d.Cvid)
                    .HasConstraintName("FK__Applicant__CVID__267ABA7A");
            });

            modelBuilder.Entity<Apply>(entity =>
            {
                entity.HasKey(e => new { e.ApplicantId, e.CompanyId })
                    .HasName("PK__Apply__EB77E08C1B5371A0");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apply__Applicant__2B3F6F97");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apply__CompanyID__2C3393D0");
            });

            modelBuilder.Entity<WebCv>(entity =>
            {
                entity.HasKey(e => e.Cvid)
                    .HasName("PK__WebCV__A04CFC437D913F45");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
