using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> entity)
        {
            entity.ToTable("Job", "HangFire");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Job_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.StateId)
                .IsRequired(false)
                .HasColumnName("StateId");

            entity.Property(e => e.StateName)
                .IsRequired(false)
                .HasMaxLength(20)
                .HasColumnName("StateName");

            entity.Property(e => e.InvocationData)
                .IsRequired(true)
                .HasColumnName("InvocationData");

            entity.Property(e => e.Arguments)
                .IsRequired(true)
                .HasColumnName("Arguments");

            entity.Property(e => e.CreatedAt)
                .IsRequired(true)
                .HasColumnName("CreatedAt");

            entity.Property(e => e.ExpireAt)
                .IsRequired(false)
                .HasColumnName("ExpireAt");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Job> entity);
    }
}
