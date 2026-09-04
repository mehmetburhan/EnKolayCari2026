using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class CountryHolidaysConfiguration : IEntityTypeConfiguration<CountryHolidays>
    {
        public void Configure(EntityTypeBuilder<CountryHolidays> entity)
        {
            entity.ToTable("CountryHolidays", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CountryHolidays_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.CountryCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("CountryCode");

            entity.Property(e => e.AppCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("AppCode");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.HolidayDate)
                .IsRequired(true)
                .HasColumnName("HolidayDate");

            entity.Property(e => e.HolidayName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("HolidayName");

            entity.Property(e => e.HolidayType)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasDefaultValueSql("('NATIONAL')")
                .HasColumnName("HolidayType");

            entity.Property(e => e.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedDate");

            entity.Property(e => e.CreatedUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CreatedUser");

            entity.Property(e => e.ModifedDate)
                .IsRequired(false)
                .HasColumnName("ModifedDate");

            entity.Property(e => e.ModifedUser)
                .IsRequired(false)
                .HasColumnName("ModifedUser");

            entity.Property(e => e.DeletedDate)
                .IsRequired(false)
                .HasColumnName("DeletedDate");

            entity.Property(e => e.DeletedUser)
                .IsRequired(false)
                .HasColumnName("DeletedUser");

            entity.Property(e => e.IsDelete)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsDelete");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CountryHolidays> entity);
    }
}
