using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class HashConfiguration : IEntityTypeConfiguration<Hash>
    {
        public void Configure(EntityTypeBuilder<Hash> entity)
        {
            entity.ToTable("Hash", "HangFire");

            // ✅ Primary Key: Key + Field
            entity.HasKey(e => new { e.Key, e.Field }).HasName("Hash_pk");

            entity.Property(e => e.Key)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Key");

            entity.Property(e => e.Field)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Field");

            entity.Property(e => e.Value)
                .IsRequired(false)
                .HasColumnName("Value");

            entity.Property(e => e.ExpireAt)
                .IsRequired(false)
                .HasColumnName("ExpireAt");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Hash> entity);
    }
}
