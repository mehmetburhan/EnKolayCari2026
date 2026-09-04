using System;

namespace EnKolayCari2026.Application.Model.DTO.Finance
{
    public class AccountDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public int AccountType { get; set; }
        public bool? Stat { get; set; }
        public string AccountCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullNameOrTitle { get; set; }
        public string BankAccountHolder { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CorrespondenceEmail { get; set; }
        public bool IsActivationCompleted { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string BankBranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public bool? IsETaxpayerActive { get; set; }
        public decimal DiscountRate { get; set; }
        public long? PriceGroupId { get; set; }
        public string IntegrationName { get; set; }
        public string IntegrationId { get; set; }
        public string Label { get; set; }
        public bool? ShowIbanOnEcommerce { get; set; }
        public bool NewsletterOptIn { get; set; }
        public bool SmsOptIn { get; set; }
        public bool MembershipAgreementAccepted { get; set; }
        public bool KvkkAccepted { get; set; }
        public int RecordSource { get; set; }
        public string NationalId { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool FreeShipping { get; set; }
        public long? TrendyolId { get; set; }
        public string TrendyolDescription { get; set; }
        public int PaymentTermDays { get; set; }
        public string SocialSecurity { get; set; }
        public string AdditionalInfo { get; set; }
        public string KvkkApprovalCode { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
    }
}
