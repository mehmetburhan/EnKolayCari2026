using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.HangFire;

namespace EnKolayCari.Persistence.Context.Configuration.HangFire
{
    public partial class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            entity.ToTable("State", "HangFire");

            // ✅ Primary Key: Id + JobId
            entity.HasKey(e => new { e.Id, e.JobId }).HasName("State_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.JobId)
                .IsRequired(true)
                .HasColumnName("JobId");

            entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(20)
                .HasColumnName("Name");

            entity.Property(e => e.Reason)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Reason");

            entity.Property(e => e.CreatedAt)
                .IsRequired(true)
                .HasColumnName("CreatedAt");

            entity.Property(e => e.Data)
                .IsRequired(false)
                .HasColumnName("Data");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<State> entity);
    }
}
