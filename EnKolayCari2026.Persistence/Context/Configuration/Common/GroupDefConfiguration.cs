using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class GroupDefConfiguration : IEntityTypeConfiguration<GroupDef>
    {
        public void Configure(EntityTypeBuilder<GroupDef> entity)
        {
            entity.ToTable("GroupDef", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("GroupDef_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.Code)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("Code");

            entity.Property(e => e.Description)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Description");

            entity.Property(e => e.Order)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
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

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.GroupDef)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_GroupDef_Company");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<GroupDef> entity);
    }
}
