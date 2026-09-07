using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class CentralCurrencyRateConfiguration : IEntityTypeConfiguration<CentralCurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CentralCurrencyRate> entity)
        {
            entity.ToTable("CentralCurrencyRate", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CentralCurrencyRate_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.TransactionDate)
                .IsRequired(true)
                .HasColumnName("TransactionDate");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.Value)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("Value");

            entity.Property(e => e.DefaultCurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("DefaultCurrencyCode");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CentralCurrencyRate> entity);
    }
}
