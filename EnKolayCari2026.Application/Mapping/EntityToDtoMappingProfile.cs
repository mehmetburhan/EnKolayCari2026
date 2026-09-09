using AutoMapper;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;
using EnKolayCari2026.Application.Model.DTO.Common;
using EnKolayCari2026.Application.Model.DTO.Dbo;
using EnKolayCari2026.Application.Model.DTO.Finance;
using EnKolayCari2026.Application.Model.DTO.HangFire;
using EnKolayCari2026.Application.Model.DTO.Inventory;
using EnKolayCari2026.Application.Model.DTO.Report;
using EnKolayCari2026.Application.Model.DTO.Trade;

namespace EnKolayCari2026.Application.Mapping
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            //Common
            CreateMap<AppExceptionLog, AppExceptionLogDto>();
            CreateMap<AppSettings, AppSettingsDto>();
            CreateMap<Branch, BranchDto>();
            CreateMap<CentralCurrency, CentralCurrencyDto>();
            CreateMap<CentralCurrencyRate, CentralCurrencyRateDto>();
            CreateMap<City, CityDto>();
            CreateMap<CodeDef, CodeDefDto>();
            CreateMap<Company, CompanyDto>();
            CreateMap<Counter, CounterDto>();
            CreateMap<CounterReference, CounterReferenceDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryHolidays, CountryHolidaysDto>();
            CreateMap<EmailTemplate, EmailTemplateDto>();
            CreateMap<FileBlob, FileBlobDto>();
            CreateMap<FileHeader, FileHeaderDto>();
            CreateMap<GroupDef, GroupDefDto>();
            CreateMap<GroupDefCountry, GroupDefCountryDto>();
            CreateMap<LabelDef, LabelDefDto>();
            CreateMap<LabelPool, LabelPoolDto>();
            CreateMap<Languages, LanguagesDto>();
            CreateMap<LegacyRole, LegacyRoleDto>();
            CreateMap<LegacyTransferMap, LegacyTransferMapDto>();
            CreateMap<License, LicenseDto>();
            CreateMap<LicenseType, LicenseTypeDto>();
            CreateMap<MailLog, MailLogDto>();
            CreateMap<Moduls, ModulsDto>();
            CreateMap<PageDef, PageDefDto>();
            CreateMap<Personal, PersonalDto>();
            CreateMap<PersonalBranch, PersonalBranchDto>();
            CreateMap<PersonalCompany, PersonalCompanyDto>();
            CreateMap<PersonalGroup, PersonalGroupDto>();
            CreateMap<PersonalGroupDef, PersonalGroupDefDto>();
            CreateMap<PersonalLoginActivity, PersonalLoginActivityDto>();
            CreateMap<PersonalWidget, PersonalWidgetDto>();
            CreateMap<ProcessFlow, ProcessFlowDto>();
            CreateMap<ProcessType, ProcessTypeDto>();
            CreateMap<ProcessTypePersonal, ProcessTypePersonalDto>();
            CreateMap<Token, TokenDto>();
            CreateMap<TranslationDef, TranslationDefDto>();
            //Dbo
            CreateMap<AspNetRoleClaims, AspNetRoleClaimsDto>();
            CreateMap<AspNetRoles, AspNetRolesDto>();
            CreateMap<AspNetUserClaims, AspNetUserClaimsDto>();
            CreateMap<AspNetUserLogins, AspNetUserLoginsDto>();
            CreateMap<AspNetUserRoles, AspNetUserRolesDto>();
            CreateMap<AspNetUsers, AspNetUsersDto>();
            CreateMap<AspNetUserTokens, AspNetUserTokensDto>();
            CreateMap<MenuRole, MenuRoleDto>();
            //Finance
            CreateMap<Account, AccountDto>();
            CreateMap<AccountAddress, AccountAddressDto>();
            CreateMap<AccountDocument, AccountDocumentDto>();
            CreateMap<AccountTransaction, AccountTransactionDto>();
            CreateMap<CashRegister, CashRegisterDto>();
            CreateMap<CashRegisterPersonal, CashRegisterPersonalDto>();
            CreateMap<CashTransaction, CashTransactionDto>();
            CreateMap<CheckNote, CheckNoteDto>();
            CreateMap<CheckNoteTransaction, CheckNoteTransactionDto>();
            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyRate, CurrencyRateDto>();
            //HangFire
            CreateMap<AggregatedCounter, AggregatedCounterDto>();
            CreateMap<Counter, CounterDto>();
            CreateMap<Hash, HashDto>();
            CreateMap<Job, JobDto>();
            CreateMap<JobParameter, JobParameterDto>();
            CreateMap<JobQueue, JobQueueDto>();
            CreateMap<List, ListDto>();
            CreateMap<Schema, SchemaDto>();
            CreateMap<Server, ServerDto>();
            CreateMap<Set, SetDto>();
            CreateMap<State, StateDto>();
            //Inventory
            CreateMap<Brand, BrandDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductBarcode, ProductBarcodeDto>();
            CreateMap<ProductColorPalette, ProductColorPaletteDto>();
            CreateMap<ProductUnit, ProductUnitDto>();
            CreateMap<StockCount, StockCountDto>();
            CreateMap<StockCountLine, StockCountLineDto>();
            CreateMap<Store, StoreDto>();
            CreateMap<StorePersonal, StorePersonalDto>();
            CreateMap<Unit, UnitDto>();
            //Report
            CreateMap<ReportDef, ReportDefDto>();
            //Trade
            CreateMap<TradeDocument, TradeDocumentDto>();
            CreateMap<TradeDocumentCurrency, TradeDocumentCurrencyDto>();
            CreateMap<TradeDocumentLine, TradeDocumentLineDto>();
            CreateMap<TradeDocumentLineTemp, TradeDocumentLineTempDto>();
            CreateMap<TradeDocumentTemp, TradeDocumentTempDto>();
        }
    }
}
