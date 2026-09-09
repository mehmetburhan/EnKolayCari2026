using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Persistence.Context.Configuration.Trade
{
    public partial class TradeDocumentLineTempConfiguration : IEntityTypeConfiguration<TradeDocumentLineTemp>
    {
        public void Configure(EntityTypeBuilder<TradeDocumentLineTemp> entity)
        {
            entity.ToTable("TradeDocumentLineTemp", "trade");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("TradeDocumentLineTemp_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.PersonalGId)
                .IsRequired(true)
                .HasColumnName("PersonalGId");

            entity.Property(e => e.CartId)
                .IsRequired(false)
                .HasColumnName("CartId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.DocumentType)
                .IsRequired(true)
                .HasColumnName("DocumentType");

            entity.Property(e => e.ProductGId)
                .IsRequired(true)
                .HasColumnName("ProductGId");

            entity.Property(e => e.ProductDescription)
                .IsRequired(true)
                .HasMaxLength(500)
                .HasColumnName("ProductDescription");

            entity.Property(e => e.SerialNo)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("SerialNo");

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

            entity.Property(e => e.UnitMultiplier)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("UnitMultiplier");

            entity.Property(e => e.UnitPrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("UnitPrice");

            entity.Property(e => e.UnitPriceVatIncluded)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("UnitPriceVatIncluded");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.VatRate)
                .IsRequired(true)
                .HasColumnName("VatRate");

            entity.Property(e => e.VatDH)
                .IsRequired(false)
                .HasMaxLength(1)
                .HasColumnName("VatDH");

            entity.Property(e => e.TotalAmountExVat)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("TotalAmountExVat");

            entity.Property(e => e.TotalAmount)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("TotalAmount");

            entity.Property(e => e.TradeDocumentGId)
                .IsRequired(false)
                .HasColumnName("TradeDocumentGId");

            entity.Property(e => e.TradeDocumentTypeGId)
                .IsRequired(false)
                .HasColumnName("TradeDocumentTypeGId");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.EcommercePaymentId)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("EcommercePaymentId");

            entity.Property(e => e.IsSoldOut)
                .IsRequired(true)
                .HasColumnName("IsSoldOut");

            entity.Property(e => e.StoreGId)
                .IsRequired(false)
                .HasColumnName("StoreGId");

            entity.Property(e => e.DiscountPercent)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("DiscountPercent");

            entity.Property(e => e.Discount1Percent)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("Discount1Percent");

            entity.Property(e => e.DeferralPercent)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("DeferralPercent");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.TradeDocumentLineTemp)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_TradeDocumentLineTemp_CompanyId");

            entity.HasOne(d => d.Unit)
                  .WithMany(p => p.TradeDocumentLineTemp)
                  .HasForeignKey(d => d.UnitId)
                  .HasConstraintName("FK_TradeDocumentLineTemp_UnitId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TradeDocumentLineTemp> entity);
    }
}
