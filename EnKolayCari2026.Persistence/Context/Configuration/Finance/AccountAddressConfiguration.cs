using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Finance;

namespace EnKolayCari2026.Persistence.Context.Configuration.Finance
{
    public partial class AccountAddressConfiguration : IEntityTypeConfiguration<AccountAddress>
    {
        public void Configure(EntityTypeBuilder<AccountAddress> entity)
        {
            entity.ToTable("AccountAddress", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("AccountAddress_pk");

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

            entity.Property(e => e.AccountId)
                .IsRequired(true)
                .HasColumnName("AccountId");

            entity.Property(e => e.AddressTitle)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("AddressTitle");

            entity.Property(e => e.AdSoyad)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("AdSoyad");

            entity.Property(e => e.Address)
                .IsRequired(true)
                .HasColumnName("Address");

            entity.Property(e => e.City)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("City");

            entity.Property(e => e.District)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("District");

            entity.Property(e => e.PostalCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("PostalCode");

            entity.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Email");

            entity.Property(e => e.Phone)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Phone");

            entity.Property(e => e.InsertUser)
                .IsRequired(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.AccountAddress)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_AccountAddress_CompanyId");

            entity.HasOne(d => d.Account)
                  .WithMany(p => p.AccountAddress)
                  .HasForeignKey(d => d.AccountId)
                  .HasConstraintName("FK_AccountAddress_AccountId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AccountAddress> entity);
    }
}
