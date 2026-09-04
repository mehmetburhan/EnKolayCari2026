using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class ModulsConfiguration : IEntityTypeConfiguration<Moduls>
    {
        public void Configure(EntityTypeBuilder<Moduls> entity)
        {
            entity.ToTable("Moduls", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Moduls_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.ModulName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("ModulName");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Moduls> entity);
    }
}
