using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Dbo;

namespace EnKolayCari.Persistence.Context.Configuration.Dbo
{
    public partial class AspNetUserTokensConfiguration : IEntityTypeConfiguration<AspNetUserTokens>
    {
        public void Configure(EntityTypeBuilder<AspNetUserTokens> entity)
        {
            entity.ToTable("AspNetUserTokens", "dbo");

            // ✅ Primary Key: UserId + LoginProvider + Name
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("AspNetUserTokens_pk");

            entity.Property(e => e.UserId)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("UserId");

            entity.Property(e => e.LoginProvider)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("LoginProvider");

            entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasColumnName("Name");

            entity.Property(e => e.Value)
                .IsRequired(false)
                .HasColumnName("Value");

            entity.Property(e => e.ExpireDate)
                .IsRequired(false)
                .HasColumnName("ExpireDate");

            //Foreign Key
            entity.HasOne(d => d.AspNetUsers)
                  .WithMany(p => p.AspNetUserTokens)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AspNetUserTokens> entity);
    }
}
