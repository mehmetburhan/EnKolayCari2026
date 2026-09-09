using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Finance;

namespace EnKolayCari2026.Persistence.Context.Configuration.Finance
{
    public partial class CheckNoteConfiguration : IEntityTypeConfiguration<CheckNote>
    {
        public void Configure(EntityTypeBuilder<CheckNote> entity)
        {
            entity.ToTable("CheckNote", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CheckNote_pk");

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

            entity.Property(e => e.AccountId)
                .IsRequired(true)
                .HasColumnName("AccountId");

            entity.Property(e => e.KimdeAccountDefId)
                .IsRequired(true)
                .HasColumnName("KimdeAccountDefId");

            entity.Property(e => e.DocumentNo)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("DocumentNo");

            entity.Property(e => e.DocumentStatus)
                .IsRequired(true)
                .HasColumnName("DocumentStatus");

            entity.Property(e => e.TransactionDate)
                .IsRequired(true)
                .HasColumnName("TransactionDate");

            entity.Property(e => e.DueDate)
                .IsRequired(true)
                .HasColumnName("DueDate");

            entity.Property(e => e.NoteType)
                .IsRequired(true)
                .HasColumnName("NoteType");

            entity.Property(e => e.Amount)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("Amount");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.DocumentOriginalOwnerTitle)
                .IsRequired(false)
                .HasMaxLength(150)
                .HasColumnName("DocumentOriginalOwnerTitle");

            entity.Property(e => e.TCKN)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("TCKN");

            entity.Property(e => e.OwningBank)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("OwningBank");

            entity.Property(e => e.BankBranch)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("BankBranch");

            entity.Property(e => e.KesideYeri)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("KesideYeri");

            entity.Property(e => e.IBAN)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("IBAN");

            entity.Property(e => e.MersisNumber)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("MersisNumber");

            entity.Property(e => e.FindeksNo)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("FindeksNo");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Label");

            entity.Property(e => e.InsertDatetime)
                .IsRequired(true)
                .HasColumnName("InsertDatetime");

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
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.CheckNote)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_CheckNote_CompanyId");

            entity.HasOne(d => d.Account)
                  .WithMany(p => p.CheckNote)
                  .HasForeignKey(d => d.AccountId)
                  .HasConstraintName("FK_CheckNote_AccountId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CheckNote> entity);
    }
}
