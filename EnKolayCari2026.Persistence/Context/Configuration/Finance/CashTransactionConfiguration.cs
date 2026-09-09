using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Finance;

namespace EnKolayCari2026.Persistence.Context.Configuration.Finance
{
    public partial class CashTransactionConfiguration : IEntityTypeConfiguration<CashTransaction>
    {
        public void Configure(EntityTypeBuilder<CashTransaction> entity)
        {
            entity.ToTable("CashTransaction", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CashTransaction_pk");

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

            entity.Property(e => e.CashRegisterId)
                .IsRequired(true)
                .HasColumnName("CashRegisterId");

            entity.Property(e => e.AccountTransactionId)
                .IsRequired(true)
                .HasColumnName("AccountTransactionId");

            entity.Property(e => e.TransactionDate)
                .IsRequired(true)
                .HasColumnName("TransactionDate");

            entity.Property(e => e.CashRegisterTransactionType)
                .IsRequired(true)
                .HasColumnName("CashRegisterTransactionType");

            entity.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(1000)
                .HasColumnName("Description");

            entity.Property(e => e.DebitAmount)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("DebitAmount");

            entity.Property(e => e.CreditAmount)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("CreditAmount");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Label");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.InsertUser)
                .IsRequired(true)
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
                  .WithMany(p => p.CashTransaction)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_CashTransaction_CompanyId");

            entity.HasOne(d => d.CashRegister)
                  .WithMany(p => p.CashTransaction)
                  .HasForeignKey(d => d.CashRegisterId)
                  .HasConstraintName("FK_CashTransaction_CashRegisterId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CashTransaction> entity);
    }
}
