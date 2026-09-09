using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class Personal  //Personel/kullanici karti (AI mimari). Eski: PersonelTanim. Tenant: CompanyMasterCode.
    {
        public long Id { get; set; }  //Birincil anahtar (Identity). Eski: PersonelTanim.Id
        public Guid GId { get; set; }  //Kuresel benzersiz kimlik. Eski: PersonelTanim.GId
        public string CompanyMasterCode { get; set; }  //Sirket master kodu (tenant). Eski: PersonelTanim.SirketTanimId -> Company.CompanyMasterCode
        public bool Stat { get; set; }  //Aktif/pasif. Eski: PersonelTanim.Aktif
        public string Name { get; set; }  //Ad. Eski: PersonelTanim.AdSoyad (bolunmus)
        public string SurName { get; set; }  //Soyad. Eski: PersonelTanim.AdSoyad (bolunmus)
        public bool UseEMailForLogin { get; set; }  //Domain kullanicisi olmayanlar icin e-posta ile giris. | Eski alan: PersonelTanim e-posta giris modu
        public string Email { get; set; }  //Giris e-postasi. Eski: PersonelTanim.Email
        public string DomainUserId { get; set; }  //Domain kullanici Id. Eski: PersonelTanim (dogrudan alan yok)
        public bool IsWebEnabled { get; set; } 
        public bool IsBranchManager { get; set; } 
        public string ManagerAccountCode { get; set; } 
        public string CostCenter { get; set; } 
        public string PhoneNumber { get; set; }  //Telefon. Eski: PersonelTanim.Telefon
        public long? BranchId { get; set; } 
        public long? ParentPersonalId { get; set; } 
        public string IdentityNumber { get; set; } 
        public string ManagerIdentityNumber { get; set; } 
        public DateTime? StartWorkDate { get; set; } 
        public DateTime? EndWorkDate { get; set; } 
        public string AppCode { get; set; } 
        public long? AppCodeId { get; set; } 
        public string CountryCode { get; set; } 
        public string UserId { get; set; }  //AspNetUsers.Id (e-posta girisi). Eski: PersonelTanim Identity baglantisi
        public string UserIdForDomain { get; set; }  //AspNetUsers.Id (domain girisi).
        public string DeviceId { get; set; } 
        public string CustomClientOs { get; set; } 
        public string CustomClientAppVersion { get; set; } 
        public string Color { get; set; }  //Takvim/UI rengi. Eski: PersonelTanim.Renk
        public string ResetPassCode { get; set; } 
        public DateTime? ResetPassExpireDate { get; set; }  //Sifre sifirlama kodu son kullanma. Eski: PersonelTanim.PasswordResetCodeExpirationMinutes
        public string EmplId { get; set; } 
        public string Division { get; set; } 
        public string PositionDesc { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public long CreatedUser { get; set; } 
        public DateTime? ModifedDate { get; set; } 
        public long? ModifedUser { get; set; } 
        public DateTime? DeletedDate { get; set; } 
        public long? DeletedUser { get; set; } 
        public long IsDelete { get; set; } 
        public virtual ICollection<CashRegisterPersonal> CashRegisterPersonal { get; set; } = new List<CashRegisterPersonal>();
        public virtual ICollection<AppExceptionLog> AppExceptionLog { get; set; } = new List<AppExceptionLog>();
        public virtual ICollection<MailLog> MailLog { get; set; } = new List<MailLog>();
        public virtual ICollection<PersonalLoginActivity> PersonalLoginActivity { get; set; } = new List<PersonalLoginActivity>();
        public virtual ICollection<PersonalWidget> PersonalWidget { get; set; } = new List<PersonalWidget>();
        public virtual ICollection<ProcessTypePersonal> ProcessTypePersonal { get; set; } = new List<ProcessTypePersonal>();
        public virtual ICollection<PersonalGroup> PersonalGroup { get; set; } = new List<PersonalGroup>();
        public virtual ICollection<PersonalGroupDef> PersonalGroupDef { get; set; } = new List<PersonalGroupDef>();
        public virtual ICollection<PersonalCompany> PersonalCompany { get; set; } = new List<PersonalCompany>();
        public virtual ICollection<StorePersonal> StorePersonal { get; set; } = new List<StorePersonal>();
        public virtual ICollection<PersonalBranch> PersonalBranch { get; set; } = new List<PersonalBranch>();
        public virtual ICollection<Token> Token { get; set; } = new List<Token>();
    }
}
