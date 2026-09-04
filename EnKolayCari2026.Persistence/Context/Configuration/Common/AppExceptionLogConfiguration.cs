using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class AppExceptionLogConfiguration : IEntityTypeConfiguration<AppExceptionLog>
    {
        public void Configure(EntityTypeBuilder<AppExceptionLog> entity)
        {
            entity.ToTable("AppExceptionLog", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AppExceptionLog_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(false)
                .HasColumnName("CompanyId");

            entity.Property(e => e.PersonalId)
                .IsRequired(false)
                .HasColumnName("PersonalId");

            entity.Property(e => e.DeviceId)
                .IsRequired(true)
                .HasMaxLength(500)
                .HasColumnName("DeviceId");

            entity.Property(e => e.ClientOs)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ClientOs");

            entity.Property(e => e.AppVersion)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("AppVersion");

            entity.Property(e => e.Language)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("Language");

            entity.Property(e => e.IpAddress)
                .IsRequired(false)
                .HasMaxLength(60)
                .HasColumnName("IpAddress");

            entity.Property(e => e.ExceptionType)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("ExceptionType");

            entity.Property(e => e.ExceptionMessage)
                .IsRequired(false)
                .HasColumnName("ExceptionMessage");

            entity.Property(e => e.StackTrace)
                .IsRequired(false)
                .HasColumnName("StackTrace");

            entity.Property(e => e.Source)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Source");

            entity.Property(e => e.PageOrScreen)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("PageOrScreen");

            entity.Property(e => e.ActionOrMethod)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("ActionOrMethod");

            entity.Property(e => e.RequestUrl)
                .IsRequired(false)
                .HasMaxLength(2000)
                .HasColumnName("RequestUrl");

            entity.Property(e => e.RequestMethod)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("RequestMethod");

            entity.Property(e => e.SeverityLevel)
                .IsRequired(false)
                .HasMaxLength(20)
                .HasDefaultValueSql("(N'Error')")
                .HasColumnName("SeverityLevel");

            entity.Property(e => e.AdditionalData)
                .IsRequired(false)
                .HasColumnName("AdditionalData");

            entity.Property(e => e.OccurredAt)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("OccurredAt");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedDate");

            entity.Property(e => e.CreatedUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CreatedUser");

            entity.Property(e => e.ModifedDate)
                .IsRequired(false)
                .HasColumnName("ModifedDate");

            entity.Property(e => e.ModifedUser)
                .IsRequired(false)
                .HasColumnName("ModifedUser");

            entity.Property(e => e.DeletedDate)
                .IsRequired(false)
                .HasColumnName("DeletedDate");

            entity.Property(e => e.DeletedUser)
                .IsRequired(false)
                .HasColumnName("DeletedUser");

            entity.Property(e => e.IsDelete)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsDelete");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.AppExceptionLog)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_AppExceptionLog_Company");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.AppExceptionLog)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("FK_AppExceptionLog_Personal");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AppExceptionLog> entity);
    }
}
