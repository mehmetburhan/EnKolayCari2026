using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.HangFire;

namespace EnKolayCari.Persistence.Context.Configuration.HangFire
{
    public partial class CounterConfiguration : IEntityTypeConfiguration<Counter>
    {
        public void Configure(EntityTypeBuilder<Counter> entity)
        {
            entity.ToTable("Counter", "HangFire");

            // ✅ Primary Key: Key + Id
            entity.HasKey(e => new { e.Key, e.Id }).HasName("Counter_pk");

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

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Counter> entity);
    }
}
