using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class LabelDefConfiguration : IEntityTypeConfiguration<LabelDef>
    {
        public void Configure(EntityTypeBuilder<LabelDef> entity)
        {
            entity.ToTable("LabelDef", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("LabelDef_pk");

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

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasColumnName("Label");

            entity.Property(e => e.LabelDescription)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("LabelDescription");

            entity.Property(e => e.SeoKey)
                .IsRequired(false)
                .HasColumnName("SeoKey");

            entity.Property(e => e.IsMainCategory)
                .IsRequired(false)
                .HasColumnName("IsMainCategory");

            entity.Property(e => e.Order)
                .IsRequired(false)
                .HasColumnName("Order");

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

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<LabelDef> entity);
    }
}
