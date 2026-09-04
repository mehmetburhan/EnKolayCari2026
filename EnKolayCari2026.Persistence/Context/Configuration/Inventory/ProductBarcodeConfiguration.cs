using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Inventory;

namespace EnKolayCari2026.Persistence.Context.Configuration.Inventory
{
    public partial class ProductBarcodeConfiguration : IEntityTypeConfiguration<ProductBarcode>
    {
        public void Configure(EntityTypeBuilder<ProductBarcode> entity)
        {
            entity.ToTable("ProductBarcode", "inventory");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("ProductBarcode_pk");

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

            entity.Property(e => e.ProductId)
                .IsRequired(true)
                .HasColumnName("ProductId");

            entity.Property(e => e.SortOrder)
                .IsRequired(true)
                .HasColumnName("SortOrder");

            entity.Property(e => e.IsMainProduct)
                .IsRequired(true)
                .HasColumnName("IsMainProduct");

            entity.Property(e => e.ColorSize)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("ColorSize");

            entity.Property(e => e.Size)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Size");

            entity.Property(e => e.Color)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Color");

            entity.Property(e => e.ProductColorPaletteId)
                .IsRequired(false)
                .HasColumnName("ProductColorPaletteId");

            entity.Property(e => e.Barcode)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Barcode");

            entity.Property(e => e.IsSalePriceActive)
                .IsRequired(true)
                .HasColumnName("IsSalePriceActive");

            entity.Property(e => e.SalePrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("SalePrice");

            entity.Property(e => e.InstallmentSalePrice)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("InstallmentSalePrice");

            entity.Property(e => e.PreviousSalePrice)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("PreviousSalePrice");

            entity.Property(e => e.InsertUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            entity.Property(e => e.HepsiBuradaProductId)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("HepsiBuradaProductId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProductBarcode> entity);
    }
}
