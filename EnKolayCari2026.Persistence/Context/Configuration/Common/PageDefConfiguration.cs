using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class PageDefConfiguration : IEntityTypeConfiguration<PageDef>
    {
        public void Configure(EntityTypeBuilder<PageDef> entity)
        {
            entity.ToTable("PageDef", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("PageDef_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("Description");

            entity.Property(e => e.Url)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("Url");

            entity.Property(e => e.LicenseType)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("LicenseType");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PageDef> entity);
    }
}
