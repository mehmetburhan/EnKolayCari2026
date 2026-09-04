using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Dbo;

namespace EnKolayCari2026.Persistence.Context.Configuration.Dbo
{
    public partial class MenuRoleConfiguration : IEntityTypeConfiguration<MenuRole>
    {
        public void Configure(EntityTypeBuilder<MenuRole> entity)
        {
            entity.ToTable("MenuRole", "dbo");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("MenuRole_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.MenuKey)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("MenuKey");

            entity.Property(e => e.RoleId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("RoleId");

            entity.Property(e => e.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedDate");

            entity.Property(e => e.CreatedUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CreatedUser");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MenuRole> entity);
    }
}
