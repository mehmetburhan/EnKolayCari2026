using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Finance;

namespace EnKolayCari2026.Persistence.Context.Configuration.Finance
{
    public partial class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> entity)
        {
            entity.ToTable("Account", "finance");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Account_pk");

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

            entity.Property(e => e.AccountType)
                .IsRequired(true)
                .HasColumnName("AccountType");

            entity.Property(e => e.Stat)
                .IsRequired(false)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.AccountCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("AccountCode");

            entity.Property(e => e.FirstName)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("FirstName");

            entity.Property(e => e.LastName)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("LastName");

            entity.Property(e => e.FullNameOrTitle)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("FullNameOrTitle");

            entity.Property(e => e.BankAccountHolder)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("BankAccountHolder");

            entity.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Email");

            entity.Property(e => e.Password)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Password");

            entity.Property(e => e.CorrespondenceEmail)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("CorrespondenceEmail");

            entity.Property(e => e.IsActivationCompleted)
                .IsRequired(true)
                .HasColumnName("IsActivationCompleted");

            entity.Property(e => e.WebsiteUrl)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("WebsiteUrl");

            entity.Property(e => e.Address)
                .IsRequired(false)
                .HasColumnName("Address");

            entity.Property(e => e.City)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("City");

            entity.Property(e => e.District)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("District");

            entity.Property(e => e.PostalCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("PostalCode");

            entity.Property(e => e.TaxOffice)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TaxOffice");

            entity.Property(e => e.TaxNumber)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TaxNumber");

            entity.Property(e => e.Phone1)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Phone1");

            entity.Property(e => e.Phone2)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Phone2");

            entity.Property(e => e.Fax)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Fax");

            entity.Property(e => e.BankBranchCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("BankBranchCode");

            entity.Property(e => e.AccountNumber)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("AccountNumber");

            entity.Property(e => e.IBAN)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("IBAN");

            entity.Property(e => e.IsETaxpayerActive)
                .IsRequired(false)
                .HasColumnName("IsETaxpayerActive");

            entity.Property(e => e.DiscountRate)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("DiscountRate");

            entity.Property(e => e.PriceGroupId)
                .IsRequired(false)
                .HasColumnName("PriceGroupId");

            entity.Property(e => e.IntegrationName)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("IntegrationName");

            entity.Property(e => e.IntegrationId)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("IntegrationId");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Label");

            entity.Property(e => e.ShowIbanOnEcommerce)
                .IsRequired(false)
                .HasColumnName("ShowIbanOnEcommerce");

            entity.Property(e => e.NewsletterOptIn)
                .IsRequired(true)
                .HasColumnName("NewsletterOptIn");

            entity.Property(e => e.SmsOptIn)
                .IsRequired(true)
                .HasColumnName("SmsOptIn");

            entity.Property(e => e.MembershipAgreementAccepted)
                .IsRequired(true)
                .HasColumnName("MembershipAgreementAccepted");

            entity.Property(e => e.KvkkAccepted)
                .IsRequired(true)
                .HasColumnName("KvkkAccepted");

            entity.Property(e => e.RecordSource)
                .IsRequired(true)
                .HasColumnName("RecordSource");

            entity.Property(e => e.NationalId)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("NationalId");

            entity.Property(e => e.Gender)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("Gender");

            entity.Property(e => e.BirthDate)
                .IsRequired(false)
                .HasColumnName("BirthDate");

            entity.Property(e => e.FreeShipping)
                .IsRequired(true)
                .HasColumnName("FreeShipping");

            entity.Property(e => e.TrendyolId)
                .IsRequired(false)
                .HasColumnName("TrendyolId");

            entity.Property(e => e.TrendyolDescription)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("TrendyolDescription");

            entity.Property(e => e.PaymentTermDays)
                .IsRequired(true)
                .HasColumnName("PaymentTermDays");

            entity.Property(e => e.SocialSecurity)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("SocialSecurity");

            entity.Property(e => e.AdditionalInfo)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("AdditionalInfo");

            entity.Property(e => e.KvkkApprovalCode)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("KvkkApprovalCode");

            entity.Property(e => e.InsertUser)
                .IsRequired(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

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

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.Account)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_Account_CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Account> entity);
    }
}
