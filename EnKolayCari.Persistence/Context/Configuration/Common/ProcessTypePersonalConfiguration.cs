using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class ProcessTypePersonalConfiguration : IEntityTypeConfiguration<ProcessTypePersonal>
    {
        public void Configure(EntityTypeBuilder<ProcessTypePersonal> entity)
        {
            entity.ToTable("ProcessTypePersonal", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("ProcessTypePersonal_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.ProcessTypeId)
                .IsRequired(true)
                .HasColumnName("ProcessTypeId");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

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
                  .WithMany(p => p.ProcessTypePersonal)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("ProcessTypePersonal_fk");

            entity.HasOne(d => d.ProcessType)
                  .WithMany(p => p.ProcessTypePersonal)
                  .HasForeignKey(d => d.ProcessTypeId)
                  .HasConstraintName("ProcessTypePersonal_fk2");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.ProcessTypePersonal)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("ProcessTypePersonal_fk_Personal");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProcessTypePersonal> entity);
    }
}
