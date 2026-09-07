using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Country", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Country_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.CountryCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CountryCode");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.Description)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Description");

            entity.Property(e => e.Ordered)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Ordered");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Country> entity);
    }
}
