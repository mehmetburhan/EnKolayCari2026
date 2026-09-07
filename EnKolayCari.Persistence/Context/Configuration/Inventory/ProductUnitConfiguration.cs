using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Inventory;

namespace EnKolayCari.Persistence.Context.Configuration.Inventory
{
    public partial class ProductUnitConfiguration : IEntityTypeConfiguration<ProductUnit>
    {
        public void Configure(EntityTypeBuilder<ProductUnit> entity)
        {
            entity.ToTable("ProductUnit", "inventory");

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

            entity.Property(e => e.ProductId)
                .IsRequired(true)
                .HasColumnName("ProductId");

            entity.Property(e => e.UnitId)
                .IsRequired(true)
                .HasColumnName("UnitId");

            entity.Property(e => e.Barcode)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Barcode");

            entity.Property(e => e.Carpan)
                .IsRequired(true)
                .HasColumnName("Carpan");

            entity.Property(e => e.Hassasiyet)
                .IsRequired(true)
                .HasColumnName("Hassasiyet");

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

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProductUnit> entity);
    }
}
