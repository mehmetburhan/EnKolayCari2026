using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class ProcessTypeConfiguration : IEntityTypeConfiguration<ProcessType>
    {
        public void Configure(EntityTypeBuilder<ProcessType> entity)
        {
            entity.ToTable("ProcessType", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("ProcessType_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.ModulId)
                .IsRequired(true)
                .HasColumnName("ModulId");

            entity.Property(e => e.Code)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("Code");

            entity.Property(e => e.DescriptionTR)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("DescriptionTR");

            entity.Property(e => e.DescriptionEN)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("DescriptionEN");

            entity.Property(e => e.Ordered)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Ordered");

            entity.Property(e => e.Color)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("Color");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProcessType> entity);
    }
}
