using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class ProcessFlowConfiguration : IEntityTypeConfiguration<ProcessFlow>
    {
        public void Configure(EntityTypeBuilder<ProcessFlow> entity)
        {
            entity.ToTable("ProcessFlow", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("ProcessFlow_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.ModulId)
                .IsRequired(true)
                .HasColumnName("ModulId");

            entity.Property(e => e.FromCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("FromCode");

            entity.Property(e => e.ToCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("ToCode");

            //Foreign Key
            entity.HasOne(d => d.Moduls)
                  .WithMany(p => p.ProcessFlow)
                  .HasForeignKey(d => d.ModulId)
                  .HasConstraintName("FK_ProcessFlow_Moduls");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProcessFlow> entity);
    }
}
