using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Inventory;

namespace EnKolayCari2026.Persistence.Context.Configuration.Inventory
{
    public partial class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.ToTable("Store", "inventory");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Store_pk");

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

            entity.Property(e => e.IsEcommerceSaleActive)
                .IsRequired(true)
                .HasColumnName("IsEcommerceSaleActive");

            entity.Property(e => e.IsMainStore)
                .IsRequired(true)
                .HasColumnName("IsMainStore");

            entity.Property(e => e.StoreCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("StoreCode");

            entity.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Description");

            entity.Property(e => e.AuthorizedPerson)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("AuthorizedPerson");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasColumnName("Label");

            entity.Property(e => e.Address)
                .IsRequired(false)
                .HasMaxLength(1000)
                .HasColumnName("Address");

            entity.Property(e => e.City)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("City");

            entity.Property(e => e.District)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("District");

            entity.Property(e => e.Phone1)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Phone1");

            entity.Property(e => e.Phone2)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Phone2");

            entity.Property(e => e.Fax)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Fax");

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
                  .WithMany(p => p.Store)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_Store_CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Store> entity);
    }
}
