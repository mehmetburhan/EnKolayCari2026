using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class JobQueueConfiguration : IEntityTypeConfiguration<JobQueue>
    {
        public void Configure(EntityTypeBuilder<JobQueue> entity)
        {
            entity.ToTable("JobQueue", "HangFire");

            // ✅ Primary Key: Id + Queue
            entity.HasKey(e => new { e.Id, e.Queue }).HasName("JobQueue_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.JobId)
                .IsRequired(true)
                .HasColumnName("JobId");

            entity.Property(e => e.Queue)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("Queue");

            entity.Property(e => e.FetchedAt)
                .IsRequired(false)
                .HasColumnName("FetchedAt");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<JobQueue> entity);
    }
}
