using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class JobParameterConfiguration : IEntityTypeConfiguration<JobParameter>
    {
        public void Configure(EntityTypeBuilder<JobParameter> entity)
        {
            entity.ToTable("JobParameter", "HangFire");

            // ✅ Primary Key: JobId + Name
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("JobParameter_pk");

            entity.Property(e => e.JobId)
                .IsRequired(true)
                .HasColumnName("JobId");

            entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(40)
                .HasColumnName("Name");

            entity.Property(e => e.Value)
                .IsRequired(false)
                .HasColumnName("Value");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<JobParameter> entity);
    }
}
