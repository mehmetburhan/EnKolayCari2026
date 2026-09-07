using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Persistence.Context.Configuration.Trade
{
    public partial class TradeDocumentLineConfiguration : IEntityTypeConfiguration<TradeDocumentLine>
    {
        public void Configure(EntityTypeBuilder<TradeDocumentLine> entity)
        {
            entity.ToTable("TradeDocumentLine", "trade");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("TradeDocumentLine_pk");

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
                .IsRequired(true)
                .HasColumnName("TradeDocumentId");

            entity.Property(e => e.StoreId)
                .IsRequired(true)
                .HasColumnName("StoreId");

            entity.Property(e => e.HareketType)
                .IsRequired(true)
                .HasColumnName("HareketType");

            entity.Property(e => e.ProductId)
                .IsRequired(true)
                .HasColumnName("ProductId");

            entity.Property(e => e.ProductDescription)
                .IsRequired(true)
                .HasMaxLength(500)
                .HasColumnName("ProductDescription");

            entity.Property(e => e.SeriNo)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("SeriNo");

            entity.Property(e => e.ColorSize)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("ColorSize");

            entity.Property(e => e.Quantity)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("Quantity");

            entity.Property(e => e.UnitId)
                .IsRequired(true)
                .HasColumnName("UnitId");

            entity.Property(e => e.UnitKatsayi)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("UnitKatsayi");

            entity.Property(e => e.UnitPrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("UnitPrice");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.VatRate)
                .IsRequired(true)
                .HasColumnName("VatRate");

            entity.Property(e => e.VatTutari)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("VatTutari");

            entity.Property(e => e.SatirTutari)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("SatirTutari");

            entity.Property(e => e.StokSayimiDahilEtme)
                .IsRequired(false)
                .HasColumnName("StokSayimiDahilEtme");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Label");

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

            entity.Property(e => e.DiscountPercent)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("DiscountPercent");

            entity.Property(e => e.Discount1Percent)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("Discount1Percent");

            entity.Property(e => e.VadeFarkiPercent)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("VadeFarkiPercent");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.TradeDocumentLine)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_TradeDocumentLine_CompanyId");

            entity.HasOne(d => d.TradeDocument)
                  .WithMany(p => p.TradeDocumentLine)
                  .HasForeignKey(d => d.TradeDocumentId)
                  .HasConstraintName("FK_TradeDocumentLine_TradeDocumentId");

            entity.HasOne(d => d.Store)
                  .WithMany(p => p.TradeDocumentLine)
                  .HasForeignKey(d => d.StoreId)
                  .HasConstraintName("FK_TradeDocumentLine_StoreId");

            entity.HasOne(d => d.Product)
                  .WithMany(p => p.TradeDocumentLine)
                  .HasForeignKey(d => d.ProductId)
                  .HasConstraintName("FK_TradeDocumentLine_ProductId");

            entity.HasOne(d => d.Unit)
                  .WithMany(p => p.TradeDocumentLine)
                  .HasForeignKey(d => d.UnitId)
                  .HasConstraintName("FK_TradeDocumentLine_UnitId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TradeDocumentLine> entity);
    }
}
