using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class PersonalBranchConfiguration : IEntityTypeConfiguration<PersonalBranch>
    {
        public void Configure(EntityTypeBuilder<PersonalBranch> entity)
        {
            entity.ToTable("PersonalBranch", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("PersonalBranch_pk");

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

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.BranchId)
                .IsRequired(true)
                .HasColumnName("BranchId");

            entity.Property(e => e.IsPrimary)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsPrimary");

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
            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.PersonalBranch)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("PersonalBranch_Personal_fk");

            entity.HasOne(d => d.Branch)
                  .WithMany(p => p.PersonalBranch)
                  .HasForeignKey(d => d.BranchId)
                  .HasConstraintName("PersonalBranch_Branch_fk");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PersonalBranch> entity);
    }
}
