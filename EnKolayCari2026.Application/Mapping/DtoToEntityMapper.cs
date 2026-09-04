using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
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

using CommonCounter = EnKolayCari2026.Domain.Model.Common.Counter;
using HangFireCounter = EnKolayCari2026.Domain.Model.HangFire.Counter;
using HangFireHash = EnKolayCari2026.Domain.Model.HangFire.Hash;
using HangFireJob = EnKolayCari2026.Domain.Model.HangFire.Job;
using HangFireList = EnKolayCari2026.Domain.Model.HangFire.List;
using HangFireSchema = EnKolayCari2026.Domain.Model.HangFire.Schema;
using HangFireServer = EnKolayCari2026.Domain.Model.HangFire.Server;
using HangFireSet = EnKolayCari2026.Domain.Model.HangFire.Set;
using HangFireState = EnKolayCari2026.Domain.Model.HangFire.State;
using CommonCounterDto = EnKolayCari2026.Application.Model.DTO.Common.CounterDto;
using HangFireCounterDto = EnKolayCari2026.Application.Model.DTO.HangFire.CounterDto;

namespace EnKolayCari2026.Application.Mapping
{
    public static class DtoToEntityMapper
    {
        private static readonly IMapper _mapper;

        static DtoToEntityMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
               cfg.AddProfile(new DtoToEntityMappingProfile());
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

        //Common
        public static AppExceptionLog ToEntity(this AppExceptionLogDto dto)
        {
            return _mapper.Map<AppExceptionLog>(dto);
        }

        public static AppSettings ToEntity(this AppSettingsDto dto)
        {
            return _mapper.Map<AppSettings>(dto);
        }

        public static Branch ToEntity(this BranchDto dto)
        {
            return _mapper.Map<Branch>(dto);
        }

        public static CentralCurrency ToEntity(this CentralCurrencyDto dto)
        {
            return _mapper.Map<CentralCurrency>(dto);
        }

        public static CentralCurrencyRate ToEntity(this CentralCurrencyRateDto dto)
        {
            return _mapper.Map<CentralCurrencyRate>(dto);
        }

        public static City ToEntity(this CityDto dto)
        {
            return _mapper.Map<City>(dto);
        }

        public static CodeDef ToEntity(this CodeDefDto dto)
        {
            return _mapper.Map<CodeDef>(dto);
        }

        public static Company ToEntity(this CompanyDto dto)
        {
            return _mapper.Map<Company>(dto);
        }

        public static CommonCounter ToEntity(this CommonCounterDto dto)
        {
            return _mapper.Map<CommonCounter>(dto);
        }

        public static CounterReference ToEntity(this CounterReferenceDto dto)
        {
            return _mapper.Map<CounterReference>(dto);
        }

        public static Country ToEntity(this CountryDto dto)
        {
            return _mapper.Map<Country>(dto);
        }

        public static CountryHolidays ToEntity(this CountryHolidaysDto dto)
        {
            return _mapper.Map<CountryHolidays>(dto);
        }

        public static EmailTemplate ToEntity(this EmailTemplateDto dto)
        {
            return _mapper.Map<EmailTemplate>(dto);
        }

        public static FileBlob ToEntity(this FileBlobDto dto)
        {
            return _mapper.Map<FileBlob>(dto);
        }

        public static FileHeader ToEntity(this FileHeaderDto dto)
        {
            return _mapper.Map<FileHeader>(dto);
        }

        public static GroupDef ToEntity(this GroupDefDto dto)
        {
            return _mapper.Map<GroupDef>(dto);
        }

        public static GroupDefCountry ToEntity(this GroupDefCountryDto dto)
        {
            return _mapper.Map<GroupDefCountry>(dto);
        }

        public static LabelDef ToEntity(this LabelDefDto dto)
        {
            return _mapper.Map<LabelDef>(dto);
        }

        public static LabelPool ToEntity(this LabelPoolDto dto)
        {
            return _mapper.Map<LabelPool>(dto);
        }

        public static Languages ToEntity(this LanguagesDto dto)
        {
            return _mapper.Map<Languages>(dto);
        }

        public static LegacyRole ToEntity(this LegacyRoleDto dto)
        {
            return _mapper.Map<LegacyRole>(dto);
        }

        public static LegacyTransferMap ToEntity(this LegacyTransferMapDto dto)
        {
            return _mapper.Map<LegacyTransferMap>(dto);
        }

        public static License ToEntity(this LicenseDto dto)
        {
            return _mapper.Map<License>(dto);
        }

        public static LicenseType ToEntity(this LicenseTypeDto dto)
        {
            return _mapper.Map<LicenseType>(dto);
        }

        public static MailLog ToEntity(this MailLogDto dto)
        {
            return _mapper.Map<MailLog>(dto);
        }

        public static Moduls ToEntity(this ModulsDto dto)
        {
            return _mapper.Map<Moduls>(dto);
        }

        public static PageDef ToEntity(this PageDefDto dto)
        {
            return _mapper.Map<PageDef>(dto);
        }

        public static Personal ToEntity(this PersonalDto dto)
        {
            return _mapper.Map<Personal>(dto);
        }

        public static PersonalBranch ToEntity(this PersonalBranchDto dto)
        {
            return _mapper.Map<PersonalBranch>(dto);
        }

        public static PersonalCompany ToEntity(this PersonalCompanyDto dto)
        {
            return _mapper.Map<PersonalCompany>(dto);
        }

        public static PersonalGroup ToEntity(this PersonalGroupDto dto)
        {
            return _mapper.Map<PersonalGroup>(dto);
        }

        public static PersonalGroupDef ToEntity(this PersonalGroupDefDto dto)
        {
            return _mapper.Map<PersonalGroupDef>(dto);
        }

        public static PersonalLoginActivity ToEntity(this PersonalLoginActivityDto dto)
        {
            return _mapper.Map<PersonalLoginActivity>(dto);
        }

        public static PersonalWidget ToEntity(this PersonalWidgetDto dto)
        {
            return _mapper.Map<PersonalWidget>(dto);
        }

        public static ProcessFlow ToEntity(this ProcessFlowDto dto)
        {
            return _mapper.Map<ProcessFlow>(dto);
        }

        public static ProcessType ToEntity(this ProcessTypeDto dto)
        {
            return _mapper.Map<ProcessType>(dto);
        }

        public static ProcessTypePersonal ToEntity(this ProcessTypePersonalDto dto)
        {
            return _mapper.Map<ProcessTypePersonal>(dto);
        }

        public static Token ToEntity(this TokenDto dto)
        {
            return _mapper.Map<Token>(dto);
        }

        public static TranslationDef ToEntity(this TranslationDefDto dto)
        {
            return _mapper.Map<TranslationDef>(dto);
        }

        //Dbo
        public static AspNetRoleClaims ToEntity(this AspNetRoleClaimsDto dto)
        {
            return _mapper.Map<AspNetRoleClaims>(dto);
        }

        public static AspNetRoles ToEntity(this AspNetRolesDto dto)
        {
            return _mapper.Map<AspNetRoles>(dto);
        }

        public static AspNetUserClaims ToEntity(this AspNetUserClaimsDto dto)
        {
            return _mapper.Map<AspNetUserClaims>(dto);
        }

        public static AspNetUserLogins ToEntity(this AspNetUserLoginsDto dto)
        {
            return _mapper.Map<AspNetUserLogins>(dto);
        }

        public static AspNetUserRoles ToEntity(this AspNetUserRolesDto dto)
        {
            return _mapper.Map<AspNetUserRoles>(dto);
        }

        public static AspNetUsers ToEntity(this AspNetUsersDto dto)
        {
            return _mapper.Map<AspNetUsers>(dto);
        }

        public static AspNetUserTokens ToEntity(this AspNetUserTokensDto dto)
        {
            return _mapper.Map<AspNetUserTokens>(dto);
        }

        public static MenuRole ToEntity(this MenuRoleDto dto)
        {
            return _mapper.Map<MenuRole>(dto);
        }

        //Finance
        public static Account ToEntity(this AccountDto dto)
        {
            return _mapper.Map<Account>(dto);
        }

        public static AccountAddress ToEntity(this AccountAddressDto dto)
        {
            return _mapper.Map<AccountAddress>(dto);
        }

        public static AccountDocument ToEntity(this AccountDocumentDto dto)
        {
            return _mapper.Map<AccountDocument>(dto);
        }

        public static AccountTransaction ToEntity(this AccountTransactionDto dto)
        {
            return _mapper.Map<AccountTransaction>(dto);
        }

        public static CashRegister ToEntity(this CashRegisterDto dto)
        {
            return _mapper.Map<CashRegister>(dto);
        }

        public static CashRegisterPersonal ToEntity(this CashRegisterPersonalDto dto)
        {
            return _mapper.Map<CashRegisterPersonal>(dto);
        }

        public static CashTransaction ToEntity(this CashTransactionDto dto)
        {
            return _mapper.Map<CashTransaction>(dto);
        }

        public static CheckNote ToEntity(this CheckNoteDto dto)
        {
            return _mapper.Map<CheckNote>(dto);
        }

        public static CheckNoteTransaction ToEntity(this CheckNoteTransactionDto dto)
        {
            return _mapper.Map<CheckNoteTransaction>(dto);
        }

        public static Currency ToEntity(this CurrencyDto dto)
        {
            return _mapper.Map<Currency>(dto);
        }

        public static CurrencyRate ToEntity(this CurrencyRateDto dto)
        {
            return _mapper.Map<CurrencyRate>(dto);
        }

        //HangFire
        public static AggregatedCounter ToEntity(this AggregatedCounterDto dto)
        {
            return _mapper.Map<AggregatedCounter>(dto);
        }

        public static HangFireCounter ToEntity(this HangFireCounterDto dto)
        {
            return _mapper.Map<HangFireCounter>(dto);
        }

        public static HangFireHash ToEntity(this HashDto dto)
        {
            return _mapper.Map<HangFireHash>(dto);
        }

        public static HangFireJob ToEntity(this JobDto dto)
        {
            return _mapper.Map<HangFireJob>(dto);
        }

        public static JobParameter ToEntity(this JobParameterDto dto)
        {
            return _mapper.Map<JobParameter>(dto);
        }

        public static JobQueue ToEntity(this JobQueueDto dto)
        {
            return _mapper.Map<JobQueue>(dto);
        }

        public static HangFireList ToEntity(this ListDto dto)
        {
            return _mapper.Map<HangFireList>(dto);
        }

        public static HangFireSchema ToEntity(this SchemaDto dto)
        {
            return _mapper.Map<HangFireSchema>(dto);
        }

        public static HangFireServer ToEntity(this ServerDto dto)
        {
            return _mapper.Map<HangFireServer>(dto);
        }

        public static HangFireSet ToEntity(this SetDto dto)
        {
            return _mapper.Map<HangFireSet>(dto);
        }

        public static HangFireState ToEntity(this StateDto dto)
        {
            return _mapper.Map<HangFireState>(dto);
        }

        //Inventory
        public static Brand ToEntity(this BrandDto dto)
        {
            return _mapper.Map<Brand>(dto);
        }

        public static Product ToEntity(this ProductDto dto)
        {
            return _mapper.Map<Product>(dto);
        }

        public static ProductBarcode ToEntity(this ProductBarcodeDto dto)
        {
            return _mapper.Map<ProductBarcode>(dto);
        }

        public static ProductColorPalette ToEntity(this ProductColorPaletteDto dto)
        {
            return _mapper.Map<ProductColorPalette>(dto);
        }

        public static ProductUnit ToEntity(this ProductUnitDto dto)
        {
            return _mapper.Map<ProductUnit>(dto);
        }

        public static StockCount ToEntity(this StockCountDto dto)
        {
            return _mapper.Map<StockCount>(dto);
        }

        public static StockCountLine ToEntity(this StockCountLineDto dto)
        {
            return _mapper.Map<StockCountLine>(dto);
        }

        public static Store ToEntity(this StoreDto dto)
        {
            return _mapper.Map<Store>(dto);
        }

        public static StorePersonal ToEntity(this StorePersonalDto dto)
        {
            return _mapper.Map<StorePersonal>(dto);
        }

        public static Unit ToEntity(this UnitDto dto)
        {
            return _mapper.Map<Unit>(dto);
        }

        //Report
        public static ReportDef ToEntity(this ReportDefDto dto)
        {
            return _mapper.Map<ReportDef>(dto);
        }

        //Trade
        public static TradeDocument ToEntity(this TradeDocumentDto dto)
        {
            return _mapper.Map<TradeDocument>(dto);
        }

        public static TradeDocumentCurrency ToEntity(this TradeDocumentCurrencyDto dto)
        {
            return _mapper.Map<TradeDocumentCurrency>(dto);
        }

        public static TradeDocumentLine ToEntity(this TradeDocumentLineDto dto)
        {
            return _mapper.Map<TradeDocumentLine>(dto);
        }

        public static TradeDocumentLineTemp ToEntity(this TradeDocumentLineTempDto dto)
        {
            return _mapper.Map<TradeDocumentLineTemp>(dto);
        }

        public static TradeDocumentTemp ToEntity(this TradeDocumentTempDto dto)
        {
            return _mapper.Map<TradeDocumentTemp>(dto);
        }

    }
}
