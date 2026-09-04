using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class PersonalLoginActivityConfiguration : IEntityTypeConfiguration<PersonalLoginActivity>
    {
        public void Configure(EntityTypeBuilder<PersonalLoginActivity> entity)
        {
            entity.ToTable("PersonalLoginActivity", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("PersonalLoginActivity_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.ActivityType)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("ActivityType");

            entity.Property(e => e.IsSuccess)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("IsSuccess");

            entity.Property(e => e.CustomHeaderJson)
                .IsRequired(false)
                .HasColumnName("CustomHeaderJson");

            entity.Property(e => e.DeviceId)
                .IsRequired(true)
                .HasMaxLength(500)
                .HasColumnName("DeviceId");

            entity.Property(e => e.ClientOs)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ClientOs");

            entity.Property(e => e.AppVersion)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("AppVersion");

            entity.Property(e => e.Language)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasDefaultValueSql("('tr-TR')")
                .HasColumnName("Language");

            entity.Property(e => e.IpAddress)
                .IsRequired(false)
                .HasMaxLength(60)
                .HasColumnName("IpAddress");

            entity.Property(e => e.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedDate");

            //Foreign Key
            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.PersonalLoginActivity)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("PersonalLoginActivity_fk");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PersonalLoginActivity> entity);
    }
}
