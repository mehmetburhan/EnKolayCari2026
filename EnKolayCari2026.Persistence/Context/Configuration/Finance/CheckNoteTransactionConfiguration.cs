using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Finance;

namespace EnKolayCari2026.Persistence.Context.Configuration.Finance
{
    public partial class CheckNoteTransactionConfiguration : IEntityTypeConfiguration<CheckNoteTransaction>
    {
        public void Configure(EntityTypeBuilder<CheckNoteTransaction> entity)
        {
            entity.ToTable("CheckNoteTransaction", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CheckNoteTransaction_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.TransactionDate)
                .IsRequired(true)
                .HasColumnName("TransactionDate");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.CheckNoteId)
                .IsRequired(true)
                .HasColumnName("CheckNoteId");

            entity.Property(e => e.DocumentStatus)
                .IsRequired(true)
                .HasColumnName("DocumentStatus");

            entity.Property(e => e.AccountId)
                .IsRequired(true)
                .HasColumnName("AccountId");

            entity.Property(e => e.AccountTransactionId)
                .IsRequired(true)
                .HasColumnName("AccountTransactionId");

            entity.Property(e => e.CashRegisterTransactionId)
                .IsRequired(true)
                .HasColumnName("CashRegisterTransactionId");

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
                  .WithMany(p => p.CheckNoteTransaction)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_CheckNoteTransaction_CompanyId");

            entity.HasOne(d => d.Account)
                  .WithMany(p => p.CheckNoteTransaction)
                  .HasForeignKey(d => d.AccountId)
                  .HasConstraintName("FK_CheckNoteTransaction_AccountId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CheckNoteTransaction> entity);
    }
}
