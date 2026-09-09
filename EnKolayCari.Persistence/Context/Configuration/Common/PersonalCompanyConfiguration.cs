using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class PersonalCompanyConfiguration : IEntityTypeConfiguration<PersonalCompany>
    {
        public void Configure(EntityTypeBuilder<PersonalCompany> entity)
        {
            entity.ToTable("PersonalCompany", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("PersonalCompany_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.IsWebLogin)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("IsWebLogin");

            entity.Property(e => e.IsMobileLogin)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("IsMobileLogin");

            entity.Property(e => e.ShowAllAuditSession)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ShowAllAuditSession");

            entity.Property(e => e.ShowAllAuditAction)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ShowAllAuditAction");

            entity.Property(e => e.BranchActionReport)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("BranchActionReport");

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
                  .WithMany(p => p.PersonalCompany)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("PersonalCompany_fk");

            entity.HasOne(d => d.Company)
                  .WithMany(p => p.PersonalCompany)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("PersonalCompany_fk2");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PersonalCompany> entity);
    }
}
