using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Finance;

namespace EnKolayCari.Persistence.Context.Configuration.Finance
{
    public partial class CashRegisterConfiguration : IEntityTypeConfiguration<CashRegister>
    {
        public void Configure(EntityTypeBuilder<CashRegister> entity)
        {
            entity.ToTable("CashRegister", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CashRegister_pk");

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

            entity.Property(e => e.CashRegisterCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("CashRegisterCode");

            entity.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Description");

            entity.Property(e => e.StoreId)
                .IsRequired(true)
                .HasColumnName("StoreId");

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
                  .WithMany(p => p.CashRegister)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_CashRegister_CompanyId");

            entity.HasOne(d => d.Store)
                  .WithMany(p => p.CashRegister)
                  .HasForeignKey(d => d.StoreId)
                  .HasConstraintName("FK_CashRegister_StoreId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CashRegister> entity);
    }
}
