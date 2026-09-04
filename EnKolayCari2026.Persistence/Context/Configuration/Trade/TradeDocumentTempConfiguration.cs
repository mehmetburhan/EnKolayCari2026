using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Persistence.Context.Configuration.Trade
{
    public partial class TradeDocumentTempConfiguration : IEntityTypeConfiguration<TradeDocumentTemp>
    {
        public void Configure(EntityTypeBuilder<TradeDocumentTemp> entity)
        {
            entity.ToTable("TradeDocumentTemp", "trade");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("TradeDocumentTemp_pk");

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

            entity.Property(e => e.PersonalGId)
                .IsRequired(true)
                .HasColumnName("PersonalGId");

            entity.Property(e => e.HareketType)
                .IsRequired(true)
                .HasColumnName("HareketType");

            entity.Property(e => e.AccountId)
                .IsRequired(false)
                .HasColumnName("AccountId");

            entity.Property(e => e.TradeDocumentNo)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TradeDocumentNo");

            entity.Property(e => e.TransactionDate)
                .IsRequired(false)
                .HasColumnName("TransactionDate");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.Address)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Address");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.TradeDocumentTemp)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_TradeDocumentTemp_CompanyId");

            entity.HasOne(d => d.Account)
                  .WithMany(p => p.TradeDocumentTemp)
                  .HasForeignKey(d => d.AccountId)
                  .HasConstraintName("FK_TradeDocumentTemp_AccountId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TradeDocumentTemp> entity);
    }
}
