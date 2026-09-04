using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class AppSettingsConfiguration : IEntityTypeConfiguration<AppSettings>
    {
        public void Configure(EntityTypeBuilder<AppSettings> entity)
        {
            entity.ToTable("AppSettings", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AppSettings_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.AppCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("AppCode");

            entity.Property(e => e.MaxStayInsideHour)
                .IsRequired(true)
                .HasDefaultValueSql("((8))")
                .HasColumnName("MaxStayInsideHour");

            entity.Property(e => e.IOSAppUrl)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("IOSAppUrl");

            entity.Property(e => e.AndroidAppUrl)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("AndroidAppUrl");

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

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AppSettings> entity);
    }
}
