using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Persistence.Context.Configuration.Trade
{
    public partial class TradeDocumentCurrencyConfiguration : IEntityTypeConfiguration<TradeDocumentCurrency>
    {
        public void Configure(EntityTypeBuilder<TradeDocumentCurrency> entity)
        {
            entity.ToTable("TradeDocumentCurrency", "trade");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("TradeDocumentCurrency_pk");

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

            entity.Property(e => e.TradeDocumentId)
                .IsRequired(false)
                .HasColumnName("TradeDocumentId");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.Value)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("Value");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.InsertUser)
                .IsRequired(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.TradeDocumentCurrency)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_TradeDocumentCurrency_CompanyId");

            entity.HasOne(d => d.TradeDocument)
                  .WithMany(p => p.TradeDocumentCurrency)
                  .HasForeignKey(d => d.TradeDocumentId)
                  .HasConstraintName("FK_TradeDocumentCurrency_TradeDocumentId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TradeDocumentCurrency> entity);
    }
}
