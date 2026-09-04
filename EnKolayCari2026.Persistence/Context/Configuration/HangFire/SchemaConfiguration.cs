using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class SchemaConfiguration : IEntityTypeConfiguration<Schema>
    {
        public void Configure(EntityTypeBuilder<Schema> entity)
        {
            entity.ToTable("Schema", "HangFire");

            // ✅ Primary Key: Version
            entity.HasKey(e => new { e.Version }).HasName("Schema_pk");

            entity.Property(e => e.Version)
                .IsRequired(true)
                .HasColumnName("Version");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Schema> entity);
    }
}
