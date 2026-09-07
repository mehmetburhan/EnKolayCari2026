using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> entity)
        {
            entity.ToTable("Branch", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Branch_pk");

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

            entity.Property(e => e.CompanyMasterCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("CompanyMasterCode");

            entity.Property(e => e.Code)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("Code");

            entity.Property(e => e.Description)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Description");

            entity.Property(e => e.ManagerPersonalId)
                .IsRequired(false)
                .HasColumnName("ManagerPersonalId");

            entity.Property(e => e.ManagerAccountCode)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ManagerAccountCode");

            entity.Property(e => e.Country)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Country");

            entity.Property(e => e.City)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("City");

            entity.Property(e => e.District)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("District");

            entity.Property(e => e.CityCode)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("CityCode");

            entity.Property(e => e.DistrictCode)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("DistrictCode");

            entity.Property(e => e.Latitude)
                .IsRequired(true)
                .HasPrecision(18,7)
                .HasDefaultValueSql("((0.0))")
                .HasColumnName("Latitude");

            entity.Property(e => e.Longitude)
                .IsRequired(true)
                .HasPrecision(18,7)
                .HasDefaultValueSql("((0.0))")
                .HasColumnName("Longitude");

            entity.Property(e => e.StartIP)
                .IsRequired(false)
                .HasMaxLength(60)
                .HasColumnName("StartIP");

            entity.Property(e => e.EndIP)
                .IsRequired(false)
                .HasMaxLength(60)
                .HasColumnName("EndIP");

            entity.Property(e => e.Color)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("Color");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasColumnName("Label");

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

        partial void OnConfigurePartial(EntityTypeBuilder<Branch> entity);
    }
}
