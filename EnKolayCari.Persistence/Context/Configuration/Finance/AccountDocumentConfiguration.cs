using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Finance;

namespace EnKolayCari.Persistence.Context.Configuration.Finance
{
    public partial class AccountDocumentConfiguration : IEntityTypeConfiguration<AccountDocument>
    {
        public void Configure(EntityTypeBuilder<AccountDocument> entity)
        {
            entity.ToTable("AccountDocument", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AccountDocument_pk");

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

            entity.Property(e => e.DocumentDefId)
                .IsRequired(true)
                .HasColumnName("DocumentDefId");

            entity.Property(e => e.DocumentTypeDescription)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("DocumentTypeDescription");

            entity.Property(e => e.AccountId)
                .IsRequired(true)
                .HasColumnName("AccountId");

            entity.Property(e => e.DocumentContent)
                .IsRequired(true)
                .HasColumnName("DocumentContent");

            entity.Property(e => e.ApprovalType)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("ApprovalType");

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

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.AccountDocument)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_AccountDocument_CompanyId");

            entity.HasOne(d => d.Account)
                  .WithMany(p => p.AccountDocument)
                  .HasForeignKey(d => d.AccountId)
                  .HasConstraintName("FK_AccountDocument_AccountId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AccountDocument> entity);
    }
}
