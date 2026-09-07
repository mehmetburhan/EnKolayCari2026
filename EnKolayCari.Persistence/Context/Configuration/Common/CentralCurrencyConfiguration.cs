using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class CentralCurrencyConfiguration : IEntityTypeConfiguration<CentralCurrency>
    {
        public void Configure(EntityTypeBuilder<CentralCurrency> entity)
        {
            entity.ToTable("CentralCurrency", "common");

            entity.Property(e => e.SortOrder)
                .IsRequired(true)
                .HasColumnName("SortOrder");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.Code)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("Code");

            entity.Property(e => e.Info)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Info");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CentralCurrency> entity);
    }
}
