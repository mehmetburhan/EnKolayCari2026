using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class GroupDefCountryConfiguration : IEntityTypeConfiguration<GroupDefCountry>
    {
        public void Configure(EntityTypeBuilder<GroupDefCountry> entity)
        {
            entity.ToTable("GroupDefCountry", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("GroupDefCountry_pk");

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

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.GroupDefId)
                .IsRequired(true)
                .HasColumnName("GroupDefId");

            entity.Property(e => e.CountryCode)
                .IsRequired(true)
                .HasMaxLength(10)
                .HasColumnName("CountryCode");

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

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.GroupDefCountry)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_GroupDefCountry_Company");

            entity.HasOne(d => d.GroupDef)
                  .WithMany(p => p.GroupDefCountry)
                  .HasForeignKey(d => d.GroupDefId)
                  .HasConstraintName("FK_GroupDefCountry_GroupDef");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<GroupDefCountry> entity);
    }
}
