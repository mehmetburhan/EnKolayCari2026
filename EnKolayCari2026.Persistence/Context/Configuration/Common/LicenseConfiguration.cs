using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class LicenseConfiguration : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> entity)
        {
            entity.ToTable("License", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("License_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.Hediye)
                .IsRequired(false)
                .HasColumnName("Hediye");

            entity.Property(e => e.LicenseType)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("LicenseType");

            entity.Property(e => e.StartDate)
                .IsRequired(true)
                .HasColumnName("StartDate");

            entity.Property(e => e.EndDate)
                .IsRequired(true)
                .HasColumnName("EndDate");

            entity.Property(e => e.TahsilatSekli)
                .IsRequired(false)
                .HasColumnName("TahsilatSekli");

            entity.Property(e => e.TahsilatTutari)
                .IsRequired(true)
                .HasPrecision(18,2)
                .HasColumnName("TahsilatTutari");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.InsertUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.License)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_License_CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<License> entity);
    }
}
