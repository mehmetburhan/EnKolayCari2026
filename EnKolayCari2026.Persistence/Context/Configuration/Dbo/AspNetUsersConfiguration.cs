using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Dbo;

namespace EnKolayCari2026.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetUsersConfiguration : IEntityTypeConfiguration<AspNetUsers>
    {
        public void Configure(EntityTypeBuilder<AspNetUsers> entity)
        {
            entity.ToTable("AspNetUsers", "dbo");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AspNetUsers_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("Id");

            entity.Property(e => e.FullName)
                .IsRequired(true)
                .HasMaxLength(60)
                .HasColumnName("FullName");

            entity.Property(e => e.UserName)
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnName("UserName");

            entity.Property(e => e.NormalizedUserName)
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnName("NormalizedUserName");

            entity.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnName("Email");

            entity.Property(e => e.NormalizedEmail)
                .IsRequired(false)
                .HasMaxLength(256)
                .HasColumnName("NormalizedEmail");

            entity.Property(e => e.EmailConfirmed)
                .IsRequired(true)
                .HasColumnName("EmailConfirmed");

            entity.Property(e => e.PasswordHash)
                .IsRequired(false)
                .HasColumnName("PasswordHash");

            entity.Property(e => e.SecurityStamp)
                .IsRequired(false)
                .HasColumnName("SecurityStamp");

            entity.Property(e => e.ConcurrencyStamp)
                .IsRequired(false)
                .HasColumnName("ConcurrencyStamp");

            entity.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .HasColumnName("PhoneNumber");

            entity.Property(e => e.PhoneNumberConfirmed)
                .IsRequired(true)
                .HasColumnName("PhoneNumberConfirmed");

            entity.Property(e => e.TwoFactorEnabled)
                .IsRequired(true)
                .HasColumnName("TwoFactorEnabled");

            entity.Property(e => e.LockoutEnd)
                .IsRequired(false)
                .HasColumnName("LockoutEnd");

            entity.Property(e => e.LockoutEnabled)
                .IsRequired(true)
                .HasColumnName("LockoutEnabled");

            entity.Property(e => e.AccessFailedCount)
                .IsRequired(true)
                .HasColumnName("AccessFailedCount");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetUsers> entity);
    }
}
