using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Dbo;

namespace EnKolayCari2026.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetRoleClaimsConfiguration : IEntityTypeConfiguration<AspNetRoleClaims>
    {
        public void Configure(EntityTypeBuilder<AspNetRoleClaims> entity)
        {
            entity.ToTable("AspNetRoleClaims", "dbo");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AspNetRoleClaims_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.RoleId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("RoleId");

            entity.Property(e => e.ClaimType)
                .IsRequired(false)
                .HasColumnName("ClaimType");

            entity.Property(e => e.ClaimValue)
                .IsRequired(false)
                .HasColumnName("ClaimValue");

            //Foreign Key
            entity.HasOne(d => d.AspNetRoles)
                  .WithMany(p => p.AspNetRoleClaims)
                  .HasForeignKey(d => d.RoleId)
                  .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetRoleClaims> entity);
    }
}
