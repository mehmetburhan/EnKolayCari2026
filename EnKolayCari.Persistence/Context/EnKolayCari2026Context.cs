using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//Table
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;
using EnKolayCari.Persistence.Context.Configuration.Common;
using EnKolayCari.Persistence.Context.Configuration.Dbo;
using EnKolayCari.Persistence.Context.Configuration.Finance;
using EnKolayCari.Persistence.Context.Configuration.HangFire;
using EnKolayCari.Persistence.Context.Configuration.Inventory;
using EnKolayCari.Persistence.Context.Configuration.Report;
using EnKolayCari.Persistence.Context.Configuration.Trade;
// HangFire vs Common name clashes (Counter) + BCL (List/Hash/Job/...)
using CommonCounter = EnKolayCari.Domain.Model.Common.Counter;
using HangFireCounter = EnKolayCari.Domain.Model.HangFire.Counter;
using HangFireHash = EnKolayCari.Domain.Model.HangFire.Hash;
using HangFireJob = EnKolayCari.Domain.Model.HangFire.Job;
using HangFireList = EnKolayCari.Domain.Model.HangFire.List;
using HangFireSchema = EnKolayCari.Domain.Model.HangFire.Schema;
using HangFireServer = EnKolayCari.Domain.Model.HangFire.Server;
using HangFireSet = EnKolayCari.Domain.Model.HangFire.Set;
using HangFireState = EnKolayCari.Domain.Model.HangFire.State;
using CommonCounterConfiguration = EnKolayCari.Persistence.Context.Configuration.Common.CounterConfiguration;
using HangFireCounterConfiguration = EnKolayCari.Persistence.Context.Configuration.HangFire.CounterConfiguration;

namespace EnKolayCari.Persistence.Context;
public partial class EnKolayCariContext : IdentityDbContext<AspNetUsers,AspNetRoles,string,AspNetUserClaims,AspNetUserRoles,AspNetUserLogins,AspNetRoleClaims,AspNetUserTokens>
{
    public EnKolayCariContext(DbContextOptions<EnKolayCariContext> options) : base(options) { }

    #region Tables
    //Tables DbSet
    //common
    public virtual DbSet<AppExceptionLog> AppExceptionLog { get; set; }
    public virtual DbSet<AppSettings> AppSettings { get; set; }
    public virtual DbSet<Branch> Branch { get; set; }
    public virtual DbSet<CentralCurrency> CentralCurrency { get; set; }
    public virtual DbSet<CentralCurrencyRate> CentralCurrencyRate { get; set; }
    public virtual DbSet<City> City { get; set; }
    public virtual DbSet<CodeDef> CodeDef { get; set; }
    public virtual DbSet<Company> Company { get; set; }
    public virtual DbSet<CommonCounter> Counter { get; set; }
    public virtual DbSet<CounterReference> CounterReference { get; set; }
    public virtual DbSet<Country> Country { get; set; }
    public virtual DbSet<CountryHolidays> CountryHolidays { get; set; }
    public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }
    public virtual DbSet<FileBlob> FileBlob { get; set; }
    public virtual DbSet<FileHeader> FileHeader { get; set; }
    public virtual DbSet<GroupDef> GroupDef { get; set; }
    public virtual DbSet<GroupDefCountry> GroupDefCountry { get; set; }
    public virtual DbSet<LabelDef> LabelDef { get; set; }
    public virtual DbSet<LabelPool> LabelPool { get; set; }
    public virtual DbSet<Languages> Languages { get; set; }
    public virtual DbSet<LegacyRole> LegacyRole { get; set; }
    public virtual DbSet<LegacyTransferMap> LegacyTransferMap { get; set; }
    public virtual DbSet<License> License { get; set; }
    public virtual DbSet<LicenseType> LicenseType { get; set; }
    public virtual DbSet<MailLog> MailLog { get; set; }
    public virtual DbSet<Moduls> Moduls { get; set; }
    public virtual DbSet<PageDef> PageDef { get; set; }
    public virtual DbSet<Personal> Personal { get; set; }
    public virtual DbSet<PersonalBranch> PersonalBranch { get; set; }
    public virtual DbSet<PersonalCompany> PersonalCompany { get; set; }
    public virtual DbSet<PersonalGroup> PersonalGroup { get; set; }
    public virtual DbSet<PersonalGroupDef> PersonalGroupDef { get; set; }
    public virtual DbSet<PersonalLoginActivity> PersonalLoginActivity { get; set; }
    public virtual DbSet<PersonalWidget> PersonalWidget { get; set; }
    public virtual DbSet<ProcessFlow> ProcessFlow { get; set; }
    public virtual DbSet<ProcessType> ProcessType { get; set; }
    public virtual DbSet<ProcessTypePersonal> ProcessTypePersonal { get; set; }
    public virtual DbSet<Token> Token { get; set; }
    public virtual DbSet<TranslationDef> TranslationDef { get; set; }

    //dbo
    public virtual DbSet<AspNetRoleClaims> ApplicationAspNetRoleClaims { get; set; }
    public virtual DbSet<AspNetRoles> ApplicationAspNetRoles { get; set; }
    public virtual DbSet<AspNetUserClaims> ApplicationAspNetUserClaims { get; set; }
    public virtual DbSet<AspNetUserLogins> ApplicationAspNetUserLogins { get; set; }
    public virtual DbSet<AspNetUserRoles> ApplicationAspNetUserRoles { get; set; }
    public virtual DbSet<AspNetUsers> ApplicationAspNetUsers { get; set; }
    public virtual DbSet<AspNetUserTokens> ApplicationAspNetUserTokens { get; set; }
    public virtual DbSet<MenuRole> MenuRole { get; set; }

    //finance
    public virtual DbSet<Account> Account { get; set; }
    public virtual DbSet<AccountAddress> AccountAddress { get; set; }
    public virtual DbSet<AccountDocument> AccountDocument { get; set; }
    public virtual DbSet<AccountTransaction> AccountTransaction { get; set; }
    public virtual DbSet<CashRegister> CashRegister { get; set; }
    public virtual DbSet<CashRegisterPersonal> CashRegisterPersonal { get; set; }
    public virtual DbSet<CashTransaction> CashTransaction { get; set; }
    public virtual DbSet<CheckNote> CheckNote { get; set; }
    public virtual DbSet<CheckNoteTransaction> CheckNoteTransaction { get; set; }
    public virtual DbSet<Currency> Currency { get; set; }
    public virtual DbSet<CurrencyRate> CurrencyRate { get; set; }

    //HangFire
    public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
    public virtual DbSet<HangFireCounter> HangFireCounter { get; set; }
    public virtual DbSet<HangFireHash> Hash { get; set; }
    public virtual DbSet<HangFireJob> Job { get; set; }
    public virtual DbSet<JobParameter> JobParameter { get; set; }
    public virtual DbSet<JobQueue> JobQueue { get; set; }
    public virtual DbSet<HangFireList> List { get; set; }
    public virtual DbSet<HangFireSchema> Schema { get; set; }
    public virtual DbSet<HangFireServer> Server { get; set; }
    public virtual DbSet<HangFireSet> Set { get; set; }
    public virtual DbSet<HangFireState> State { get; set; }

    //inventory
    public virtual DbSet<Brand> Brand { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<ProductBarcode> ProductBarcode { get; set; }
    public virtual DbSet<ProductColorPalette> ProductColorPalette { get; set; }
    public virtual DbSet<ProductUnit> ProductUnit { get; set; }
    public virtual DbSet<StockCount> StockCount { get; set; }
    public virtual DbSet<StockCountLine> StockCountLine { get; set; }
    public virtual DbSet<Store> Store { get; set; }
    public virtual DbSet<StorePersonal> StorePersonal { get; set; }
    public virtual DbSet<Unit> Unit { get; set; }

    //report
    public virtual DbSet<ReportDef> ReportDef { get; set; }

    //trade
    public virtual DbSet<TradeDocument> TradeDocument { get; set; }
    public virtual DbSet<TradeDocumentCurrency> TradeDocumentCurrency { get; set; }
    public virtual DbSet<TradeDocumentLine> TradeDocumentLine { get; set; }
    public virtual DbSet<TradeDocumentLineTemp> TradeDocumentLineTemp { get; set; }
    public virtual DbSet<TradeDocumentTemp> TradeDocumentTemp { get; set; }

    #endregion Tables

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseCollation("Turkish_CI_AI");
        //Table Common
        modelBuilder.ApplyConfiguration(new AppExceptionLogConfiguration());
        modelBuilder.ApplyConfiguration(new AppSettingsConfiguration());
        modelBuilder.ApplyConfiguration(new BranchConfiguration());
        modelBuilder.ApplyConfiguration(new CentralCurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new CentralCurrencyRateConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new CodeDefConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new CommonCounterConfiguration());
        modelBuilder.ApplyConfiguration(new CounterReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CountryHolidaysConfiguration());
        modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
        modelBuilder.ApplyConfiguration(new FileBlobConfiguration());
        modelBuilder.ApplyConfiguration(new FileHeaderConfiguration());
        modelBuilder.ApplyConfiguration(new GroupDefConfiguration());
        modelBuilder.ApplyConfiguration(new GroupDefCountryConfiguration());
        modelBuilder.ApplyConfiguration(new LabelDefConfiguration());
        modelBuilder.ApplyConfiguration(new LabelPoolConfiguration());
        modelBuilder.ApplyConfiguration(new LanguagesConfiguration());
        modelBuilder.ApplyConfiguration(new LegacyRoleConfiguration());
        modelBuilder.ApplyConfiguration(new LegacyTransferMapConfiguration());
        modelBuilder.ApplyConfiguration(new LicenseConfiguration());
        modelBuilder.ApplyConfiguration(new LicenseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MailLogConfiguration());
        modelBuilder.ApplyConfiguration(new ModulsConfiguration());
        modelBuilder.ApplyConfiguration(new PageDefConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalBranchConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalCompanyConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalGroupConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalGroupDefConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalLoginActivityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalWidgetConfiguration());
        modelBuilder.ApplyConfiguration(new ProcessFlowConfiguration());
        modelBuilder.ApplyConfiguration(new ProcessTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProcessTypePersonalConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new TranslationDefConfiguration());

        //Table Dbo
        modelBuilder.ApplyConfiguration(new AspNetRoleClaimsConfiguration());
        modelBuilder.ApplyConfiguration(new AspNetRolesConfiguration());
        modelBuilder.ApplyConfiguration(new AspNetUserClaimsConfiguration());
        modelBuilder.ApplyConfiguration(new AspNetUserLoginsConfiguration());
        modelBuilder.ApplyConfiguration(new AspNetUserRolesConfiguration());
        modelBuilder.ApplyConfiguration(new AspNetUsersConfiguration());
        modelBuilder.ApplyConfiguration(new AspNetUserTokensConfiguration());
        modelBuilder.ApplyConfiguration(new MenuRoleConfiguration());

        //Table Finance
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountAddressConfiguration());
        modelBuilder.ApplyConfiguration(new AccountDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new CashRegisterConfiguration());
        modelBuilder.ApplyConfiguration(new CashRegisterPersonalConfiguration());
        modelBuilder.ApplyConfiguration(new CashTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new CheckNoteConfiguration());
        modelBuilder.ApplyConfiguration(new CheckNoteTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());

        //Table HangFire
        modelBuilder.ApplyConfiguration(new AggregatedCounterConfiguration());
        modelBuilder.ApplyConfiguration(new HangFireCounterConfiguration());
        modelBuilder.ApplyConfiguration(new HashConfiguration());
        modelBuilder.ApplyConfiguration(new JobConfiguration());
        modelBuilder.ApplyConfiguration(new JobParameterConfiguration());
        modelBuilder.ApplyConfiguration(new JobQueueConfiguration());
        modelBuilder.ApplyConfiguration(new ListConfiguration());
        modelBuilder.ApplyConfiguration(new SchemaConfiguration());
        modelBuilder.ApplyConfiguration(new ServerConfiguration());
        modelBuilder.ApplyConfiguration(new SetConfiguration());
        modelBuilder.ApplyConfiguration(new StateConfiguration());

        //Table Inventory
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductBarcodeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductColorPaletteConfiguration());
        modelBuilder.ApplyConfiguration(new ProductUnitConfiguration());
        modelBuilder.ApplyConfiguration(new StockCountConfiguration());
        modelBuilder.ApplyConfiguration(new StockCountLineConfiguration());
        modelBuilder.ApplyConfiguration(new StoreConfiguration());
        modelBuilder.ApplyConfiguration(new StorePersonalConfiguration());
        modelBuilder.ApplyConfiguration(new UnitConfiguration());

        //Table Report
        modelBuilder.ApplyConfiguration(new ReportDefConfiguration());

        //Table Trade
        modelBuilder.ApplyConfiguration(new TradeDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new TradeDocumentCurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new TradeDocumentLineConfiguration());
        modelBuilder.ApplyConfiguration(new TradeDocumentLineTempConfiguration());
        modelBuilder.ApplyConfiguration(new TradeDocumentTempConfiguration());



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
