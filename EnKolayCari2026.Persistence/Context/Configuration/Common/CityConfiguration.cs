using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entity)
        {
            entity.ToTable("City", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("City_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasColumnName("GId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.FullCityCode)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("FullCityCode");

            entity.Property(e => e.CountryCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CountryCode");

            entity.Property(e => e.CityCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("CityCode");

            entity.Property(e => e.ParentCityCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ParentCityCode");

            entity.Property(e => e.CityName)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("CityName");

            entity.Property(e => e.Level)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Level");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<City> entity);
    }
}
