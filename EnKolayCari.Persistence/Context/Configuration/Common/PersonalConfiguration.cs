using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class PersonalConfiguration : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> entity)
        {
            entity.ToTable("Personal", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Personal_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyMasterCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("CompanyMasterCode");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("Name");

            entity.Property(e => e.SurName)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("SurName");

            entity.Property(e => e.UseEMailForLogin)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("UseEMailForLogin");

            entity.Property(e => e.Email)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Email");

            entity.Property(e => e.DomainUserId)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("DomainUserId");

            entity.Property(e => e.IsWebEnabled)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsWebEnabled");

            entity.Property(e => e.IsBranchManager)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsBranchManager");

            entity.Property(e => e.ManagerAccountCode)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("ManagerAccountCode");

            entity.Property(e => e.CostCenter)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("CostCenter");

            entity.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("PhoneNumber");

            entity.Property(e => e.BranchId)
                .IsRequired(false)
                .HasColumnName("BranchId");

            entity.Property(e => e.ParentPersonalId)
                .IsRequired(false)
                .HasColumnName("ParentPersonalId");

            entity.Property(e => e.IdentityNumber)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("IdentityNumber");

            entity.Property(e => e.ManagerIdentityNumber)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ManagerIdentityNumber");

            entity.Property(e => e.StartWorkDate)
                .IsRequired(false)
                .HasColumnName("StartWorkDate");

            entity.Property(e => e.EndWorkDate)
                .IsRequired(false)
                .HasColumnName("EndWorkDate");

            entity.Property(e => e.AppCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("AppCode");

            entity.Property(e => e.AppCodeId)
                .IsRequired(false)
                .HasColumnName("AppCodeId");

            entity.Property(e => e.CountryCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("CountryCode");

            entity.Property(e => e.UserId)
                .IsRequired(false)
                .HasMaxLength(450)
                .HasColumnName("UserId");

            entity.Property(e => e.UserIdForDomain)
                .IsRequired(false)
                .HasMaxLength(450)
                .HasColumnName("UserIdForDomain");

            entity.Property(e => e.DeviceId)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("DeviceId");

            entity.Property(e => e.CustomClientOs)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("CustomClientOs");

            entity.Property(e => e.CustomClientAppVersion)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("CustomClientAppVersion");

            entity.Property(e => e.Color)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("Color");

            entity.Property(e => e.ResetPassCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ResetPassCode");

            entity.Property(e => e.ResetPassExpireDate)
                .IsRequired(false)
                .HasColumnName("ResetPassExpireDate");

            entity.Property(e => e.EmplId)
                .IsRequired(false)
                .HasMaxLength(20)
                .HasColumnName("EmplId");

            entity.Property(e => e.Division)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("Division");

            entity.Property(e => e.PositionDesc)
                .IsRequired(false)
                .HasMaxLength(255)
                .HasColumnName("PositionDesc");

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

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Personal> entity);
    }
}
