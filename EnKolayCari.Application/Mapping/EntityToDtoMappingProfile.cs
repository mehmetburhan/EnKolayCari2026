using AutoMapper;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;
using EnKolayCari.Application.Model.DTO.Common;
using EnKolayCari.Application.Model.DTO.Dbo;
using EnKolayCari.Application.Model.DTO.Finance;
using EnKolayCari.Application.Model.DTO.HangFire;
using EnKolayCari.Application.Model.DTO.Inventory;
using EnKolayCari.Application.Model.DTO.Report;
using EnKolayCari.Application.Model.DTO.Trade;
using CommonCounter = EnKolayCari.Domain.Model.Common.Counter;
using HangFireCounter = EnKolayCari.Domain.Model.HangFire.Counter;
using HangFireHash = EnKolayCari.Domain.Model.HangFire.Hash;
using HangFireJob = EnKolayCari.Domain.Model.HangFire.Job;
using HangFireList = EnKolayCari.Domain.Model.HangFire.List;
using HangFireSchema = EnKolayCari.Domain.Model.HangFire.Schema;
using HangFireServer = EnKolayCari.Domain.Model.HangFire.Server;
using HangFireSet = EnKolayCari.Domain.Model.HangFire.Set;
using HangFireState = EnKolayCari.Domain.Model.HangFire.State;
using CommonCounterDto = EnKolayCari.Application.Model.DTO.Common.CounterDto;
using HangFireCounterDto = EnKolayCari.Application.Model.DTO.HangFire.CounterDto;

namespace EnKolayCari.Application.Mapping
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
            CreateMap<CommonCounter, CommonCounterDto>();
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
            CreateMap<HangFireCounter, HangFireCounterDto>();
            CreateMap<HangFireHash, HashDto>();
            CreateMap<HangFireJob, JobDto>();
            CreateMap<JobParameter, JobParameterDto>();
            CreateMap<JobQueue, JobQueueDto>();
            CreateMap<HangFireList, ListDto>();
            CreateMap<HangFireSchema, SchemaDto>();
            CreateMap<HangFireServer, ServerDto>();
            CreateMap<HangFireSet, SetDto>();
            CreateMap<HangFireState, StateDto>();
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
