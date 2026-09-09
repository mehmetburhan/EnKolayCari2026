using AutoMapper;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;
using EnKolayCari2026.Application.Model.DTO.Common;
using EnKolayCari2026.Application.Model.DTO.Dbo;
using EnKolayCari2026.Application.Model.DTO.Finance;
using EnKolayCari2026.Application.Model.DTO.Inventory;
using EnKolayCari2026.Application.Model.DTO.Report;
using EnKolayCari2026.Application.Model.DTO.Trade;
namespace EnKolayCari2026.Application.Mapping
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            //Common
            CreateMap<AppExceptionLogDto, AppExceptionLog>();
            CreateMap<AppSettingsDto, AppSettings>();
            CreateMap<BranchDto, Branch>();
            CreateMap<CentralCurrencyDto, CentralCurrency>();
            CreateMap<CentralCurrencyRateDto, CentralCurrencyRate>();
            CreateMap<CityDto, City>();
            CreateMap<CodeDefDto, CodeDef>();
            CreateMap<CompanyDto, Company>();
            CreateMap<CounterDto, Counter>();
            CreateMap<CounterReferenceDto, CounterReference>();
            CreateMap<CountryDto, Country>();
            CreateMap<CountryHolidaysDto, CountryHolidays>();
            CreateMap<EmailTemplateDto, EmailTemplate>();
            CreateMap<FileBlobDto, FileBlob>();
            CreateMap<FileHeaderDto, FileHeader>();
            CreateMap<GroupDefDto, GroupDef>();
            CreateMap<GroupDefCountryDto, GroupDefCountry>();
            CreateMap<LabelDefDto, LabelDef>();
            CreateMap<LabelPoolDto, LabelPool>();
            CreateMap<LanguagesDto, Languages>();
            CreateMap<LegacyRoleDto, LegacyRole>();
            CreateMap<LegacyTransferMapDto, LegacyTransferMap>();
            CreateMap<LicenseDto, License>();
            CreateMap<LicenseTypeDto, LicenseType>();
            CreateMap<MailLogDto, MailLog>();
            CreateMap<ModulsDto, Moduls>();
            CreateMap<PageDefDto, PageDef>();
            CreateMap<PersonalDto, Personal>();
            CreateMap<PersonalBranchDto, PersonalBranch>();
            CreateMap<PersonalCompanyDto, PersonalCompany>();
            CreateMap<PersonalGroupDto, PersonalGroup>();
            CreateMap<PersonalGroupDefDto, PersonalGroupDef>();
            CreateMap<PersonalLoginActivityDto, PersonalLoginActivity>();
            CreateMap<PersonalWidgetDto, PersonalWidget>();
            CreateMap<ProcessFlowDto, ProcessFlow>();
            CreateMap<ProcessTypeDto, ProcessType>();
            CreateMap<ProcessTypePersonalDto, ProcessTypePersonal>();
            CreateMap<TokenDto, Token>();
            CreateMap<TranslationDefDto, TranslationDef>();
            //Dbo
            CreateMap<AspNetRoleClaimsDto, AspNetRoleClaims>();
            CreateMap<AspNetRolesDto, AspNetRoles>();
            CreateMap<AspNetUserClaimsDto, AspNetUserClaims>();
            CreateMap<AspNetUserLoginsDto, AspNetUserLogins>();
            CreateMap<AspNetUserRolesDto, AspNetUserRoles>();
            CreateMap<AspNetUsersDto, AspNetUsers>();
            CreateMap<AspNetUserTokensDto, AspNetUserTokens>();
            CreateMap<MenuRoleDto, MenuRole>();
            //Finance
            CreateMap<AccountDto, Account>();
            CreateMap<AccountAddressDto, AccountAddress>();
            CreateMap<AccountDocumentDto, AccountDocument>();
            CreateMap<AccountTransactionDto, AccountTransaction>();
            CreateMap<CashRegisterDto, CashRegister>();
            CreateMap<CashRegisterPersonalDto, CashRegisterPersonal>();
            CreateMap<CashTransactionDto, CashTransaction>();
            CreateMap<CheckNoteDto, CheckNote>();
            CreateMap<CheckNoteTransactionDto, CheckNoteTransaction>();
            CreateMap<CurrencyDto, Currency>();
            CreateMap<CurrencyRateDto, CurrencyRate>();
            //Inventory
            CreateMap<BrandDto, Brand>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductBarcodeDto, ProductBarcode>();
            CreateMap<ProductColorPaletteDto, ProductColorPalette>();
            CreateMap<ProductUnitDto, ProductUnit>();
            CreateMap<StockCountDto, StockCount>();
            CreateMap<StockCountLineDto, StockCountLine>();
            CreateMap<StoreDto, Store>();
            CreateMap<StorePersonalDto, StorePersonal>();
            CreateMap<UnitDto, Unit>();
            //Report
            CreateMap<ReportDefDto, ReportDef>();
            //Trade
            CreateMap<TradeDocumentDto, TradeDocument>();
            CreateMap<TradeDocumentCurrencyDto, TradeDocumentCurrency>();
            CreateMap<TradeDocumentLineDto, TradeDocumentLine>();
            CreateMap<TradeDocumentLineTempDto, TradeDocumentLineTemp>();
            CreateMap<TradeDocumentTempDto, TradeDocumentTemp>();
        }
    }
}
