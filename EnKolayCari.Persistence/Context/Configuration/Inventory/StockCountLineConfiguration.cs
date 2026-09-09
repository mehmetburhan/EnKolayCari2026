using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Inventory;

namespace EnKolayCari.Persistence.Context.Configuration.Inventory
{
    public partial class StockCountLineConfiguration : IEntityTypeConfiguration<StockCountLine>
    {
        public void Configure(EntityTypeBuilder<StockCountLine> entity)
        {
            entity.ToTable("StockCountLine", "inventory");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("StockCountLine_pk");

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

            entity.Property(e => e.StockCountId)
                .IsRequired(true)
                .HasColumnName("StockCountId");

            entity.Property(e => e.ProductId)
                .IsRequired(true)
                .HasColumnName("ProductId");

            entity.Property(e => e.Color)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Color");

            entity.Property(e => e.Barcode)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Barcode");

            entity.Property(e => e.Quantity)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("Quantity");

            entity.Property(e => e.TradeDocumentLineId)
                .IsRequired(true)
                .HasColumnName("TradeDocumentLineId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StockCountLine> entity);
    }
}
