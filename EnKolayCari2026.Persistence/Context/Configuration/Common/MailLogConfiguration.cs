using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class MailLogConfiguration : IEntityTypeConfiguration<MailLog>
    {
        public void Configure(EntityTypeBuilder<MailLog> entity)
        {
            entity.ToTable("MailLog", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("MailLog_pk");

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

            entity.Property(e => e.TableName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("TableName");

            entity.Property(e => e.TableId)
                .IsRequired(true)
                .HasColumnName("TableId");

            entity.Property(e => e.MailTypeCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("MailTypeCode");

            entity.Property(e => e.MailTo)
                .IsRequired(true)
                .HasMaxLength(500)
                .HasColumnName("MailTo");

            entity.Property(e => e.MailSubject)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("MailSubject");

            entity.Property(e => e.MailBody)
                .IsRequired(false)
                .HasColumnName("MailBody");

            entity.Property(e => e.SentDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("SentDate");

            entity.Property(e => e.SentByPersonalId)
                .IsRequired(false)
                .HasColumnName("SentByPersonalId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedDate");

            entity.Property(e => e.CreatedUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CreatedUser");

            entity.Property(e => e.ModifedDate)
                .IsRequired(false)
                .HasColumnName("ModifedDate");

            entity.Property(e => e.ModifedUser)
                .IsRequired(false)
                .HasColumnName("ModifedUser");

            entity.Property(e => e.DeletedDate)
                .IsRequired(false)
                .HasColumnName("DeletedDate");

            entity.Property(e => e.DeletedUser)
                .IsRequired(false)
                .HasColumnName("DeletedUser");

            entity.Property(e => e.IsDelete)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsDelete");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.MailLog)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_MailLog_Company");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.MailLog)
                  .HasForeignKey(d => d.SentByPersonalId)
                  .HasConstraintName("FK_MailLog_SentByPersonal");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MailLog> entity);
    }
}
