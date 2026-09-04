using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class AggregatedCounterConfiguration : IEntityTypeConfiguration<AggregatedCounter>
    {
        public void Configure(EntityTypeBuilder<AggregatedCounter> entity)
        {
            entity.ToTable("AggregatedCounter", "HangFire");

            // ✅ Primary Key: Key
            entity.HasKey(e => new { e.Key }).HasName("AggregatedCounter_pk");

            entity.Property(e => e.Key)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Key");

            entity.Property(e => e.Value)
                .IsRequired(true)
                .HasColumnName("Value");

            entity.Property(e => e.ExpireAt)
                .IsRequired(false)
                .HasColumnName("ExpireAt");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AggregatedCounter> entity);
    }
}
