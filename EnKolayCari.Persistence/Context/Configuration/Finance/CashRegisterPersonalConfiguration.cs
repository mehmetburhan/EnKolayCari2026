using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Finance;

namespace EnKolayCari.Persistence.Context.Configuration.Finance
{
    public partial class CashRegisterPersonalConfiguration : IEntityTypeConfiguration<CashRegisterPersonal>
    {
        public void Configure(EntityTypeBuilder<CashRegisterPersonal> entity)
        {
            entity.ToTable("CashRegisterPersonal", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CashRegisterPersonal_pk");

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

            entity.Property(e => e.CashRegisterId)
                .IsRequired(true)
                .HasColumnName("CashRegisterId");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.CashRegisterPersonal)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_CashRegisterPersonal_CompanyId");

            entity.HasOne(d => d.CashRegister)
                  .WithMany(p => p.CashRegisterPersonal)
                  .HasForeignKey(d => d.CashRegisterId)
                  .HasConstraintName("FK_CashRegisterPersonal_CashRegisterId");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.CashRegisterPersonal)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("FK_CashRegisterPersonal_PersonalId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CashRegisterPersonal> entity);
    }
}
