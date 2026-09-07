using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class PersonalGroupConfiguration : IEntityTypeConfiguration<PersonalGroup>
    {
        public void Configure(EntityTypeBuilder<PersonalGroup> entity)
        {
            entity.ToTable("PersonalGroup", "common");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(false)
                .HasColumnName("CompanyId");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.GroupCode)
                .IsRequired(true)
                .HasColumnName("GroupCode");

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
                  .WithMany(p => p.PersonalGroup)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("PersonalGroup_fk");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.PersonalGroup)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("PersonalGroup_fk2");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PersonalGroup> entity);
    }
}
