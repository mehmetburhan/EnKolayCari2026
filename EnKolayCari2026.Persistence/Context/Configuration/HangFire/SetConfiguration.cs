using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class SetConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> entity)
        {
            entity.ToTable("Set", "HangFire");

            // ✅ Primary Key: Key + Value
            entity.HasKey(e => new { e.Key, e.Value }).HasName("Set_pk");

            entity.Property(e => e.Key)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Key");

            entity.Property(e => e.Score)
                .IsRequired(true)
                .HasColumnName("Score");

            entity.Property(e => e.Value)
                .IsRequired(true)
                .HasMaxLength(256)
                .HasColumnName("Value");

            entity.Property(e => e.ExpireAt)
                .IsRequired(false)
                .HasColumnName("ExpireAt");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Set> entity);
    }
}
