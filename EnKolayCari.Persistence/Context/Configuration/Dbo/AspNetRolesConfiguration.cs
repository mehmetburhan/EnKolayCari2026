using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Dbo;

namespace EnKolayCari.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetRolesConfiguration : IEntityTypeConfiguration<AspNetRoles>
    {
        public void Configure(EntityTypeBuilder<AspNetRoles> entity)
        {
            entity.ToTable("AspNetRoles", "dbo");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AspNetRoles_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");

            entity.Property(e => e.Name)
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnName("Name");

            entity.Property(e => e.NormalizedName)
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnName("NormalizedName");

            entity.Property(e => e.ConcurrencyStamp)
                .IsRequired(false)
                .HasColumnName("ConcurrencyStamp");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetRoles> entity);
    }
}
