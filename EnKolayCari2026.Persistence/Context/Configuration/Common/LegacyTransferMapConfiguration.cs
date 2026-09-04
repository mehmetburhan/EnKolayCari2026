using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class LegacyTransferMapConfiguration : IEntityTypeConfiguration<LegacyTransferMap>
    {
        public void Configure(EntityTypeBuilder<LegacyTransferMap> entity)
        {
            entity.ToTable("LegacyTransferMap", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("LegacyTransferMap_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.LegacyTableName)
                .IsRequired(true)
                .HasMaxLength(128)
                .HasColumnName("LegacyTableName");

            entity.Property(e => e.LegacyGId)
                .IsRequired(true)
                .HasColumnName("LegacyGId");

            entity.Property(e => e.LegacyLastChangeDate)
                .IsRequired(false)
                .HasColumnName("LegacyLastChangeDate");

            entity.Property(e => e.NewTableName)
                .IsRequired(true)
                .HasMaxLength(128)
                .HasColumnName("NewTableName");

            entity.Property(e => e.NewGId)
                .IsRequired(true)
                .HasColumnName("NewGId");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<LegacyTransferMap> entity);
    }
}
