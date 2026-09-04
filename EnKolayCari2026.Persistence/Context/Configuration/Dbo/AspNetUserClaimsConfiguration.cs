using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Dbo;

namespace EnKolayCari2026.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetUserClaimsConfiguration : IEntityTypeConfiguration<AspNetUserClaims>
    {
        public void Configure(EntityTypeBuilder<AspNetUserClaims> entity)
        {
            entity.ToTable("AspNetUserClaims", "dbo");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AspNetUserClaims_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.UserId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("UserId");

            entity.Property(e => e.ClaimType)
                .IsRequired(false)
                .HasColumnName("ClaimType");

            entity.Property(e => e.ClaimValue)
                .IsRequired(false)
                .HasColumnName("ClaimValue");

            //Foreign Key
            entity.HasOne(d => d.AspNetUsers)
                  .WithMany(p => p.AspNetUserClaims)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetUserClaims> entity);
    }
}
