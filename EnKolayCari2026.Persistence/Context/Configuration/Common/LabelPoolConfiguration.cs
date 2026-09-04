using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class LabelPoolConfiguration : IEntityTypeConfiguration<LabelPool>
    {
        public void Configure(EntityTypeBuilder<LabelPool> entity)
        {
            entity.ToTable("LabelPool", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("LabelPool_pk");

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
                .HasMaxLength(30)
                .HasColumnName("TableName");

            entity.Property(e => e.TableId)
                .IsRequired(true)
                .HasColumnName("TableId");

            entity.Property(e => e.Label)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("Label");

            entity.Property(e => e.LabelDefId)
                .IsRequired(false)
                .HasColumnName("LabelDefId");

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
            entity.HasOne(d => d.LabelDef)
                  .WithMany(p => p.LabelPool)
                  .HasForeignKey(d => d.LabelDefId)
                  .HasConstraintName("LabelPool_fk_LabelDef");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<LabelPool> entity);
    }
}
