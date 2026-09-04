using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class PersonalGroupDefConfiguration : IEntityTypeConfiguration<PersonalGroupDef>
    {
        public void Configure(EntityTypeBuilder<PersonalGroupDef> entity)
        {
            entity.ToTable("PersonalGroupDef", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("PersonalGroupDef_pk");

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

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.GroupDefId)
                .IsRequired(true)
                .HasColumnName("GroupDefId");

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
                  .WithMany(p => p.PersonalGroupDef)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_PersonalGroupDef_Company");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.PersonalGroupDef)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("FK_PersonalGroupDef_Personal");

            entity.HasOne(d => d.GroupDef)
                  .WithMany(p => p.PersonalGroupDef)
                  .HasForeignKey(d => d.GroupDefId)
                  .HasConstraintName("FK_PersonalGroupDef_GroupDef");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PersonalGroupDef> entity);
    }
}
