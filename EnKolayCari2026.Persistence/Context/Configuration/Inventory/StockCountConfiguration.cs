using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Inventory;

namespace EnKolayCari2026.Persistence.Context.Configuration.Inventory
{
    public partial class StockCountConfiguration : IEntityTypeConfiguration<StockCount>
    {
        public void Configure(EntityTypeBuilder<StockCount> entity)
        {
            entity.ToTable("StockCount", "inventory");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("StockCount_pk");

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

            entity.Property(e => e.StockCountDate)
                .IsRequired(true)
                .HasColumnName("StockCountDate");

            entity.Property(e => e.StockCountDescription)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("StockCountDescription");

            entity.Property(e => e.StockCountStatus)
                .IsRequired(true)
                .HasColumnName("StockCountStatus");

            entity.Property(e => e.StoreId)
                .IsRequired(true)
                .HasColumnName("StoreId");

            entity.Property(e => e.InboundTradeDocumentId)
                .IsRequired(true)
                .HasColumnName("InboundTradeDocumentId");

            entity.Property(e => e.OutboundTradeDocumentId)
                .IsRequired(true)
                .HasColumnName("OutboundTradeDocumentId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StockCount> entity);
    }
}
