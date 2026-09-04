using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class TranslationDefConfiguration : IEntityTypeConfiguration<TranslationDef>
    {
        public void Configure(EntityTypeBuilder<TranslationDef> entity)
        {
            entity.ToTable("TranslationDef", "common");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("CompanyId");

            entity.Property(e => e.TableName)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("TableName");

            entity.Property(e => e.TableId)
                .IsRequired(true)
                .HasColumnName("TableId");

            entity.Property(e => e.TableFieldName)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TableFieldName");

            entity.Property(e => e.LanguageCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("LanguageCode");

            entity.Property(e => e.Description)
                .IsRequired(true)
                .HasColumnName("Description");

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
                  .WithMany(p => p.TranslationDef)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("TranslationDef_fk");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TranslationDef> entity);
    }
}
