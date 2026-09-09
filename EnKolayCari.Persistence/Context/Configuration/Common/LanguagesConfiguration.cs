using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class LanguagesConfiguration : IEntityTypeConfiguration<Languages>
    {
        public void Configure(EntityTypeBuilder<Languages> entity)
        {
            entity.ToTable("Languages", "common");

            // ✅ Primary Key: LanguageCode
            entity.HasKey(e => new { e.LanguageCode }).HasName("Languages_pk");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.MasterLanguge)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("MasterLanguge");

            entity.Property(e => e.LanguageCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("LanguageCode");

            entity.Property(e => e.DisLanguageCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("DisLanguageCode");

            entity.Property(e => e.Description)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("Description");

            entity.Property(e => e.Order)
                .IsRequired(true)
                .HasDefaultValueSql("((99))")
                .HasColumnName("Order");

            entity.Property(e => e.Flag)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("Flag");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Languages> entity);
    }
}
