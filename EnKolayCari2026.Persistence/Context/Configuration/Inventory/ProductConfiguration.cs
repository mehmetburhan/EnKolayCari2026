using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Inventory;

namespace EnKolayCari2026.Persistence.Context.Configuration.Inventory
{
    public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product", "inventory");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Product_pk");

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

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.ProductCode)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("ProductCode");

            entity.Property(e => e.OzelKod)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("OzelKod");

            entity.Property(e => e.BrandId)
                .IsRequired(true)
                .HasColumnName("BrandId");

            entity.Property(e => e.ProductDescription)
                .IsRequired(true)
                .HasMaxLength(300)
                .HasColumnName("ProductDescription");

            entity.Property(e => e.ProductKisaBilgi)
                .IsRequired(false)
                .HasMaxLength(300)
                .HasColumnName("ProductKisaBilgi");

            entity.Property(e => e.Barcode)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Barcode");

            entity.Property(e => e.ColorId)
                .IsRequired(false)
                .HasColumnName("ColorId");

            entity.Property(e => e.IsWeightBarcode)
                .IsRequired(true)
                .HasColumnName("IsWeightBarcode");

            entity.Property(e => e.UnitId)
                .IsRequired(true)
                .HasColumnName("UnitId");

            entity.Property(e => e.CategoryId)
                .IsRequired(false)
                .HasColumnName("CategoryId");

            entity.Property(e => e.CriticalStockQuantity)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("CriticalStockQuantity");

            entity.Property(e => e.InternetCriticalStockQuantity)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("InternetCriticalStockQuantity");

            entity.Property(e => e.SeriNoTakip)
                .IsRequired(true)
                .HasColumnName("SeriNoTakip");

            entity.Property(e => e.PurchaseVatRate)
                .IsRequired(true)
                .HasColumnName("PurchaseVatRate");

            entity.Property(e => e.VatRate)
                .IsRequired(true)
                .HasColumnName("VatRate");

            entity.Property(e => e.PurchasePrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("PurchasePrice");

            entity.Property(e => e.PurchaseCurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("PurchaseCurrencyCode");

            entity.Property(e => e.PurchaseVatIncluded)
                .IsRequired(true)
                .HasMaxLength(1)
                .HasColumnName("PurchaseVatIncluded");

            entity.Property(e => e.PreviousSalePrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("PreviousSalePrice");

            entity.Property(e => e.SalePrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("SalePrice");

            entity.Property(e => e.InstallmentSalePrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("InstallmentSalePrice");

            entity.Property(e => e.SaleCurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("SaleCurrencyCode");

            entity.Property(e => e.SaleVatIncluded)
                .IsRequired(true)
                .HasMaxLength(1)
                .HasColumnName("SaleVatIncluded");

            entity.Property(e => e.ProductDetay)
                .IsRequired(false)
                .HasColumnName("ProductDetay");

            entity.Property(e => e.VideoUrl)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("VideoUrl");

            entity.Property(e => e.IntegrationName)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("IntegrationName");

            entity.Property(e => e.IntegrationId)
                .IsRequired(false)
                .HasColumnName("IntegrationId");

            entity.Property(e => e.IsInternetSaleActive)
                .IsRequired(true)
                .HasColumnName("IsInternetSaleActive");

            entity.Property(e => e.IsTrendyolActive)
                .IsRequired(true)
                .HasColumnName("IsTrendyolActive");

            entity.Property(e => e.IsHepsiBuradaActive)
                .IsRequired(true)
                .HasColumnName("IsHepsiBuradaActive");

            entity.Property(e => e.Width)
                .IsRequired(true)
                .HasColumnName("Width");

            entity.Property(e => e.Height)
                .IsRequired(true)
                .HasColumnName("Height");

            entity.Property(e => e.Derinlik)
                .IsRequired(true)
                .HasColumnName("Derinlik");

            entity.Property(e => e.Desi)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("Desi");

            entity.Property(e => e.Agirlik)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("Agirlik");

            entity.Property(e => e.Color)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Color");

            entity.Property(e => e.IsAppointmentActive)
                .IsRequired(false)
                .HasColumnName("IsAppointmentActive");

            entity.Property(e => e.IsAppointmentOpen)
                .IsRequired(false)
                .HasColumnName("IsAppointmentOpen");

            entity.Property(e => e.TrendyolSyncType)
                .IsRequired(false)
                .HasMaxLength(1)
                .HasColumnName("TrendyolSyncType");

            entity.Property(e => e.HepsiBuradaSyncType)
                .IsRequired(false)
                .HasMaxLength(1)
                .HasColumnName("HepsiBuradaSyncType");

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

            entity.Property(e => e.HepsiBuradaProductId)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("HepsiBuradaProductId");

            entity.Property(e => e.MinSaleQuantity)
                .IsRequired(true)
                .HasColumnName("MinSaleQuantity");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> entity);
    }
}
