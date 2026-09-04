using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Dbo;

namespace EnKolayCari2026.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetUserLoginsConfiguration : IEntityTypeConfiguration<AspNetUserLogins>
    {
        public void Configure(EntityTypeBuilder<AspNetUserLogins> entity)
        {
            entity.ToTable("AspNetUserLogins", "dbo");

            // ✅ Primary Key: LoginProvider + ProviderKey
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("AspNetUserLogins_pk");

            entity.Property(e => e.LoginProvider)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("LoginProvider");

            entity.Property(e => e.ProviderKey)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("ProviderKey");

            entity.Property(e => e.ProviderDisplayName)
                .IsRequired(false)
                .HasColumnName("ProviderDisplayName");

            entity.Property(e => e.UserId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("UserId");

            //Foreign Key
            entity.HasOne(d => d.AspNetUsers)
                  .WithMany(p => p.AspNetUserLogins)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetUserLogins> entity);
    }
}
