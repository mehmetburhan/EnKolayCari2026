using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Dbo;

namespace EnKolayCari.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetUserRolesConfiguration : IEntityTypeConfiguration<AspNetUserRoles>
    {
        public void Configure(EntityTypeBuilder<AspNetUserRoles> entity)
        {
            entity.ToTable("AspNetUserRoles", "dbo");

            // ✅ Primary Key: UserId + RoleId + CompanyId
            entity.HasKey(e => new { e.UserId, e.RoleId, e.CompanyId }).HasName("AspNetUserRoles_pk");

            entity.Property(e => e.UserId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("UserId");

            entity.Property(e => e.RoleId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("RoleId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetUserRoles> entity);
    }
}
