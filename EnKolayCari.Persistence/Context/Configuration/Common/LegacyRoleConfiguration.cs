using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class LegacyRoleConfiguration : IEntityTypeConfiguration<LegacyRole>
    {
        public void Configure(EntityTypeBuilder<LegacyRole> entity)
        {
            entity.ToTable("LegacyRole", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("LegacyRole_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.AppCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("AppCode");

            entity.Property(e => e.RoleCode)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("RoleCode");

            entity.Property(e => e.RoleName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("RoleName");

            entity.Property(e => e.GroupCode)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("GroupCode");

            entity.Property(e => e.SubGroupCode)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("SubGroupCode");

            entity.Property(e => e.GroupOrdered)
                .IsRequired(true)
                .HasColumnName("GroupOrdered");

            entity.Property(e => e.SubGroupOrdered)
                .IsRequired(true)
                .HasColumnName("SubGroupOrdered");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<LegacyRole> entity);
    }
}
