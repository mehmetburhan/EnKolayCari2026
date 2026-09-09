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

namespace EnKolayCari2026.Application.Mapping
{
    public static class EntityToDtoMapper
    {
        private static readonly IMapper _mapper;

        static EntityToDtoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoMappingProfile());
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();
        }

        // Common
        public static AppExceptionLogDto ToDto(this AppExceptionLog entity)
        {
            return entity == null ? default : _mapper.Map<AppExceptionLogDto>(entity);
        }

        public static List<AppExceptionLogDto> ToDtoList(this List<AppExceptionLog> entities)
        {
            return entities == null || !entities.Any() ? new List<AppExceptionLogDto>() : _mapper.Map<List<AppExceptionLogDto>>(entities);
        }

        public static List<AppExceptionLogDto> ToDtoList(this IQueryable<AppExceptionLog> entities)
        {
            return entities.Any() ? _mapper.Map<List<AppExceptionLogDto>>(entities.ToList()) : new List<AppExceptionLogDto>();
        }

        public static AppSettingsDto ToDto(this AppSettings entity)
        {
            return entity == null ? default : _mapper.Map<AppSettingsDto>(entity);
        }

        public static List<AppSettingsDto> ToDtoList(this List<AppSettings> entities)
        {
            return entities == null || !entities.Any() ? new List<AppSettingsDto>() : _mapper.Map<List<AppSettingsDto>>(entities);
        }

        public static List<AppSettingsDto> ToDtoList(this IQueryable<AppSettings> entities)
        {
            return entities.Any() ? _mapper.Map<List<AppSettingsDto>>(entities.ToList()) : new List<AppSettingsDto>();
        }

        public static BranchDto ToDto(this Branch entity)
        {
            return entity == null ? default : _mapper.Map<BranchDto>(entity);
        }

        public static List<BranchDto> ToDtoList(this List<Branch> entities)
        {
            return entities == null || !entities.Any() ? new List<BranchDto>() : _mapper.Map<List<BranchDto>>(entities);
        }

        public static List<BranchDto> ToDtoList(this IQueryable<Branch> entities)
        {
            return entities.Any() ? _mapper.Map<List<BranchDto>>(entities.ToList()) : new List<BranchDto>();
        }

        public static CentralCurrencyDto ToDto(this CentralCurrency entity)
        {
            return entity == null ? default : _mapper.Map<CentralCurrencyDto>(entity);
        }

        public static List<CentralCurrencyDto> ToDtoList(this List<CentralCurrency> entities)
        {
            return entities == null || !entities.Any() ? new List<CentralCurrencyDto>() : _mapper.Map<List<CentralCurrencyDto>>(entities);
        }

        public static List<CentralCurrencyDto> ToDtoList(this IQueryable<CentralCurrency> entities)
        {
            return entities.Any() ? _mapper.Map<List<CentralCurrencyDto>>(entities.ToList()) : new List<CentralCurrencyDto>();
        }

        public static CentralCurrencyRateDto ToDto(this CentralCurrencyRate entity)
        {
            return entity == null ? default : _mapper.Map<CentralCurrencyRateDto>(entity);
        }

        public static List<CentralCurrencyRateDto> ToDtoList(this List<CentralCurrencyRate> entities)
        {
            return entities == null || !entities.Any() ? new List<CentralCurrencyRateDto>() : _mapper.Map<List<CentralCurrencyRateDto>>(entities);
        }

        public static List<CentralCurrencyRateDto> ToDtoList(this IQueryable<CentralCurrencyRate> entities)
        {
            return entities.Any() ? _mapper.Map<List<CentralCurrencyRateDto>>(entities.ToList()) : new List<CentralCurrencyRateDto>();
        }

        public static CityDto ToDto(this City entity)
        {
            return entity == null ? default : _mapper.Map<CityDto>(entity);
        }

        public static List<CityDto> ToDtoList(this List<City> entities)
        {
            return entities == null || !entities.Any() ? new List<CityDto>() : _mapper.Map<List<CityDto>>(entities);
        }

        public static List<CityDto> ToDtoList(this IQueryable<City> entities)
        {
            return entities.Any() ? _mapper.Map<List<CityDto>>(entities.ToList()) : new List<CityDto>();
        }

        public static CodeDefDto ToDto(this CodeDef entity)
        {
            return entity == null ? default : _mapper.Map<CodeDefDto>(entity);
        }

        public static List<CodeDefDto> ToDtoList(this List<CodeDef> entities)
        {
            return entities == null || !entities.Any() ? new List<CodeDefDto>() : _mapper.Map<List<CodeDefDto>>(entities);
        }

        public static List<CodeDefDto> ToDtoList(this IQueryable<CodeDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<CodeDefDto>>(entities.ToList()) : new List<CodeDefDto>();
        }

        public static CompanyDto ToDto(this Company entity)
        {
            return entity == null ? default : _mapper.Map<CompanyDto>(entity);
        }

        public static List<CompanyDto> ToDtoList(this List<Company> entities)
        {
            return entities == null || !entities.Any() ? new List<CompanyDto>() : _mapper.Map<List<CompanyDto>>(entities);
        }

        public static List<CompanyDto> ToDtoList(this IQueryable<Company> entities)
        {
            return entities.Any() ? _mapper.Map<List<CompanyDto>>(entities.ToList()) : new List<CompanyDto>();
        }

        public static CounterDto ToDto(this Counter entity)
        {
            return entity == null ? default : _mapper.Map<CounterDto>(entity);
        }

        public static List<CounterDto> ToDtoList(this List<Counter> entities)
        {
            return entities == null || !entities.Any() ? new List<CounterDto>() : _mapper.Map<List<CounterDto>>(entities);
        }

        public static List<CounterDto> ToDtoList(this IQueryable<Counter> entities)
        {
            return entities.Any() ? _mapper.Map<List<CounterDto>>(entities.ToList()) : new List<CounterDto>();
        }

        public static CounterReferenceDto ToDto(this CounterReference entity)
        {
            return entity == null ? default : _mapper.Map<CounterReferenceDto>(entity);
        }

        public static List<CounterReferenceDto> ToDtoList(this List<CounterReference> entities)
        {
            return entities == null || !entities.Any() ? new List<CounterReferenceDto>() : _mapper.Map<List<CounterReferenceDto>>(entities);
        }

        public static List<CounterReferenceDto> ToDtoList(this IQueryable<CounterReference> entities)
        {
            return entities.Any() ? _mapper.Map<List<CounterReferenceDto>>(entities.ToList()) : new List<CounterReferenceDto>();
        }

        public static CountryDto ToDto(this Country entity)
        {
            return entity == null ? default : _mapper.Map<CountryDto>(entity);
        }

        public static List<CountryDto> ToDtoList(this List<Country> entities)
        {
            return entities == null || !entities.Any() ? new List<CountryDto>() : _mapper.Map<List<CountryDto>>(entities);
        }

        public static List<CountryDto> ToDtoList(this IQueryable<Country> entities)
        {
            return entities.Any() ? _mapper.Map<List<CountryDto>>(entities.ToList()) : new List<CountryDto>();
        }

        public static CountryHolidaysDto ToDto(this CountryHolidays entity)
        {
            return entity == null ? default : _mapper.Map<CountryHolidaysDto>(entity);
        }

        public static List<CountryHolidaysDto> ToDtoList(this List<CountryHolidays> entities)
        {
            return entities == null || !entities.Any() ? new List<CountryHolidaysDto>() : _mapper.Map<List<CountryHolidaysDto>>(entities);
        }

        public static List<CountryHolidaysDto> ToDtoList(this IQueryable<CountryHolidays> entities)
        {
            return entities.Any() ? _mapper.Map<List<CountryHolidaysDto>>(entities.ToList()) : new List<CountryHolidaysDto>();
        }

        public static EmailTemplateDto ToDto(this EmailTemplate entity)
        {
            return entity == null ? default : _mapper.Map<EmailTemplateDto>(entity);
        }

        public static List<EmailTemplateDto> ToDtoList(this List<EmailTemplate> entities)
        {
            return entities == null || !entities.Any() ? new List<EmailTemplateDto>() : _mapper.Map<List<EmailTemplateDto>>(entities);
        }

        public static List<EmailTemplateDto> ToDtoList(this IQueryable<EmailTemplate> entities)
        {
            return entities.Any() ? _mapper.Map<List<EmailTemplateDto>>(entities.ToList()) : new List<EmailTemplateDto>();
        }

        public static FileBlobDto ToDto(this FileBlob entity)
        {
            return entity == null ? default : _mapper.Map<FileBlobDto>(entity);
        }

        public static List<FileBlobDto> ToDtoList(this List<FileBlob> entities)
        {
            return entities == null || !entities.Any() ? new List<FileBlobDto>() : _mapper.Map<List<FileBlobDto>>(entities);
        }

        public static List<FileBlobDto> ToDtoList(this IQueryable<FileBlob> entities)
        {
            return entities.Any() ? _mapper.Map<List<FileBlobDto>>(entities.ToList()) : new List<FileBlobDto>();
        }

        public static FileHeaderDto ToDto(this FileHeader entity)
        {
            return entity == null ? default : _mapper.Map<FileHeaderDto>(entity);
        }

        public static List<FileHeaderDto> ToDtoList(this List<FileHeader> entities)
        {
            return entities == null || !entities.Any() ? new List<FileHeaderDto>() : _mapper.Map<List<FileHeaderDto>>(entities);
        }

        public static List<FileHeaderDto> ToDtoList(this IQueryable<FileHeader> entities)
        {
            return entities.Any() ? _mapper.Map<List<FileHeaderDto>>(entities.ToList()) : new List<FileHeaderDto>();
        }

        public static GroupDefDto ToDto(this GroupDef entity)
        {
            return entity == null ? default : _mapper.Map<GroupDefDto>(entity);
        }

        public static List<GroupDefDto> ToDtoList(this List<GroupDef> entities)
        {
            return entities == null || !entities.Any() ? new List<GroupDefDto>() : _mapper.Map<List<GroupDefDto>>(entities);
        }

        public static List<GroupDefDto> ToDtoList(this IQueryable<GroupDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<GroupDefDto>>(entities.ToList()) : new List<GroupDefDto>();
        }

        public static GroupDefCountryDto ToDto(this GroupDefCountry entity)
        {
            return entity == null ? default : _mapper.Map<GroupDefCountryDto>(entity);
        }

        public static List<GroupDefCountryDto> ToDtoList(this List<GroupDefCountry> entities)
        {
            return entities == null || !entities.Any() ? new List<GroupDefCountryDto>() : _mapper.Map<List<GroupDefCountryDto>>(entities);
        }

        public static List<GroupDefCountryDto> ToDtoList(this IQueryable<GroupDefCountry> entities)
        {
            return entities.Any() ? _mapper.Map<List<GroupDefCountryDto>>(entities.ToList()) : new List<GroupDefCountryDto>();
        }

        public static LabelDefDto ToDto(this LabelDef entity)
        {
            return entity == null ? default : _mapper.Map<LabelDefDto>(entity);
        }

        public static List<LabelDefDto> ToDtoList(this List<LabelDef> entities)
        {
            return entities == null || !entities.Any() ? new List<LabelDefDto>() : _mapper.Map<List<LabelDefDto>>(entities);
        }

        public static List<LabelDefDto> ToDtoList(this IQueryable<LabelDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<LabelDefDto>>(entities.ToList()) : new List<LabelDefDto>();
        }

        public static LabelPoolDto ToDto(this LabelPool entity)
        {
            return entity == null ? default : _mapper.Map<LabelPoolDto>(entity);
        }

        public static List<LabelPoolDto> ToDtoList(this List<LabelPool> entities)
        {
            return entities == null || !entities.Any() ? new List<LabelPoolDto>() : _mapper.Map<List<LabelPoolDto>>(entities);
        }

        public static List<LabelPoolDto> ToDtoList(this IQueryable<LabelPool> entities)
        {
            return entities.Any() ? _mapper.Map<List<LabelPoolDto>>(entities.ToList()) : new List<LabelPoolDto>();
        }

        public static LanguagesDto ToDto(this Languages entity)
        {
            return entity == null ? default : _mapper.Map<LanguagesDto>(entity);
        }

        public static List<LanguagesDto> ToDtoList(this List<Languages> entities)
        {
            return entities == null || !entities.Any() ? new List<LanguagesDto>() : _mapper.Map<List<LanguagesDto>>(entities);
        }

        public static List<LanguagesDto> ToDtoList(this IQueryable<Languages> entities)
        {
            return entities.Any() ? _mapper.Map<List<LanguagesDto>>(entities.ToList()) : new List<LanguagesDto>();
        }

        public static LegacyRoleDto ToDto(this LegacyRole entity)
        {
            return entity == null ? default : _mapper.Map<LegacyRoleDto>(entity);
        }

        public static List<LegacyRoleDto> ToDtoList(this List<LegacyRole> entities)
        {
            return entities == null || !entities.Any() ? new List<LegacyRoleDto>() : _mapper.Map<List<LegacyRoleDto>>(entities);
        }

        public static List<LegacyRoleDto> ToDtoList(this IQueryable<LegacyRole> entities)
        {
            return entities.Any() ? _mapper.Map<List<LegacyRoleDto>>(entities.ToList()) : new List<LegacyRoleDto>();
        }

        public static LegacyTransferMapDto ToDto(this LegacyTransferMap entity)
        {
            return entity == null ? default : _mapper.Map<LegacyTransferMapDto>(entity);
        }

        public static List<LegacyTransferMapDto> ToDtoList(this List<LegacyTransferMap> entities)
        {
            return entities == null || !entities.Any() ? new List<LegacyTransferMapDto>() : _mapper.Map<List<LegacyTransferMapDto>>(entities);
        }

        public static List<LegacyTransferMapDto> ToDtoList(this IQueryable<LegacyTransferMap> entities)
        {
            return entities.Any() ? _mapper.Map<List<LegacyTransferMapDto>>(entities.ToList()) : new List<LegacyTransferMapDto>();
        }

        public static LicenseDto ToDto(this License entity)
        {
            return entity == null ? default : _mapper.Map<LicenseDto>(entity);
        }

        public static List<LicenseDto> ToDtoList(this List<License> entities)
        {
            return entities == null || !entities.Any() ? new List<LicenseDto>() : _mapper.Map<List<LicenseDto>>(entities);
        }

        public static List<LicenseDto> ToDtoList(this IQueryable<License> entities)
        {
            return entities.Any() ? _mapper.Map<List<LicenseDto>>(entities.ToList()) : new List<LicenseDto>();
        }

        public static LicenseTypeDto ToDto(this LicenseType entity)
        {
            return entity == null ? default : _mapper.Map<LicenseTypeDto>(entity);
        }

        public static List<LicenseTypeDto> ToDtoList(this List<LicenseType> entities)
        {
            return entities == null || !entities.Any() ? new List<LicenseTypeDto>() : _mapper.Map<List<LicenseTypeDto>>(entities);
        }

        public static List<LicenseTypeDto> ToDtoList(this IQueryable<LicenseType> entities)
        {
            return entities.Any() ? _mapper.Map<List<LicenseTypeDto>>(entities.ToList()) : new List<LicenseTypeDto>();
        }

        public static MailLogDto ToDto(this MailLog entity)
        {
            return entity == null ? default : _mapper.Map<MailLogDto>(entity);
        }

        public static List<MailLogDto> ToDtoList(this List<MailLog> entities)
        {
            return entities == null || !entities.Any() ? new List<MailLogDto>() : _mapper.Map<List<MailLogDto>>(entities);
        }

        public static List<MailLogDto> ToDtoList(this IQueryable<MailLog> entities)
        {
            return entities.Any() ? _mapper.Map<List<MailLogDto>>(entities.ToList()) : new List<MailLogDto>();
        }

        public static ModulsDto ToDto(this Moduls entity)
        {
            return entity == null ? default : _mapper.Map<ModulsDto>(entity);
        }

        public static List<ModulsDto> ToDtoList(this List<Moduls> entities)
        {
            return entities == null || !entities.Any() ? new List<ModulsDto>() : _mapper.Map<List<ModulsDto>>(entities);
        }

        public static List<ModulsDto> ToDtoList(this IQueryable<Moduls> entities)
        {
            return entities.Any() ? _mapper.Map<List<ModulsDto>>(entities.ToList()) : new List<ModulsDto>();
        }

        public static PageDefDto ToDto(this PageDef entity)
        {
            return entity == null ? default : _mapper.Map<PageDefDto>(entity);
        }

        public static List<PageDefDto> ToDtoList(this List<PageDef> entities)
        {
            return entities == null || !entities.Any() ? new List<PageDefDto>() : _mapper.Map<List<PageDefDto>>(entities);
        }

        public static List<PageDefDto> ToDtoList(this IQueryable<PageDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<PageDefDto>>(entities.ToList()) : new List<PageDefDto>();
        }

        public static PersonalDto ToDto(this Personal entity)
        {
            return entity == null ? default : _mapper.Map<PersonalDto>(entity);
        }

        public static List<PersonalDto> ToDtoList(this List<Personal> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalDto>() : _mapper.Map<List<PersonalDto>>(entities);
        }

        public static List<PersonalDto> ToDtoList(this IQueryable<Personal> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalDto>>(entities.ToList()) : new List<PersonalDto>();
        }

        public static PersonalBranchDto ToDto(this PersonalBranch entity)
        {
            return entity == null ? default : _mapper.Map<PersonalBranchDto>(entity);
        }

        public static List<PersonalBranchDto> ToDtoList(this List<PersonalBranch> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalBranchDto>() : _mapper.Map<List<PersonalBranchDto>>(entities);
        }

        public static List<PersonalBranchDto> ToDtoList(this IQueryable<PersonalBranch> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalBranchDto>>(entities.ToList()) : new List<PersonalBranchDto>();
        }

        public static PersonalCompanyDto ToDto(this PersonalCompany entity)
        {
            return entity == null ? default : _mapper.Map<PersonalCompanyDto>(entity);
        }

        public static List<PersonalCompanyDto> ToDtoList(this List<PersonalCompany> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalCompanyDto>() : _mapper.Map<List<PersonalCompanyDto>>(entities);
        }

        public static List<PersonalCompanyDto> ToDtoList(this IQueryable<PersonalCompany> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalCompanyDto>>(entities.ToList()) : new List<PersonalCompanyDto>();
        }

        public static PersonalGroupDto ToDto(this PersonalGroup entity)
        {
            return entity == null ? default : _mapper.Map<PersonalGroupDto>(entity);
        }

        public static List<PersonalGroupDto> ToDtoList(this List<PersonalGroup> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalGroupDto>() : _mapper.Map<List<PersonalGroupDto>>(entities);
        }

        public static List<PersonalGroupDto> ToDtoList(this IQueryable<PersonalGroup> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalGroupDto>>(entities.ToList()) : new List<PersonalGroupDto>();
        }

        public static PersonalGroupDefDto ToDto(this PersonalGroupDef entity)
        {
            return entity == null ? default : _mapper.Map<PersonalGroupDefDto>(entity);
        }

        public static List<PersonalGroupDefDto> ToDtoList(this List<PersonalGroupDef> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalGroupDefDto>() : _mapper.Map<List<PersonalGroupDefDto>>(entities);
        }

        public static List<PersonalGroupDefDto> ToDtoList(this IQueryable<PersonalGroupDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalGroupDefDto>>(entities.ToList()) : new List<PersonalGroupDefDto>();
        }

        public static PersonalLoginActivityDto ToDto(this PersonalLoginActivity entity)
        {
            return entity == null ? default : _mapper.Map<PersonalLoginActivityDto>(entity);
        }

        public static List<PersonalLoginActivityDto> ToDtoList(this List<PersonalLoginActivity> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalLoginActivityDto>() : _mapper.Map<List<PersonalLoginActivityDto>>(entities);
        }

        public static List<PersonalLoginActivityDto> ToDtoList(this IQueryable<PersonalLoginActivity> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalLoginActivityDto>>(entities.ToList()) : new List<PersonalLoginActivityDto>();
        }

        public static PersonalWidgetDto ToDto(this PersonalWidget entity)
        {
            return entity == null ? default : _mapper.Map<PersonalWidgetDto>(entity);
        }

        public static List<PersonalWidgetDto> ToDtoList(this List<PersonalWidget> entities)
        {
            return entities == null || !entities.Any() ? new List<PersonalWidgetDto>() : _mapper.Map<List<PersonalWidgetDto>>(entities);
        }

        public static List<PersonalWidgetDto> ToDtoList(this IQueryable<PersonalWidget> entities)
        {
            return entities.Any() ? _mapper.Map<List<PersonalWidgetDto>>(entities.ToList()) : new List<PersonalWidgetDto>();
        }

        public static ProcessFlowDto ToDto(this ProcessFlow entity)
        {
            return entity == null ? default : _mapper.Map<ProcessFlowDto>(entity);
        }

        public static List<ProcessFlowDto> ToDtoList(this List<ProcessFlow> entities)
        {
            return entities == null || !entities.Any() ? new List<ProcessFlowDto>() : _mapper.Map<List<ProcessFlowDto>>(entities);
        }

        public static List<ProcessFlowDto> ToDtoList(this IQueryable<ProcessFlow> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProcessFlowDto>>(entities.ToList()) : new List<ProcessFlowDto>();
        }

        public static ProcessTypeDto ToDto(this ProcessType entity)
        {
            return entity == null ? default : _mapper.Map<ProcessTypeDto>(entity);
        }

        public static List<ProcessTypeDto> ToDtoList(this List<ProcessType> entities)
        {
            return entities == null || !entities.Any() ? new List<ProcessTypeDto>() : _mapper.Map<List<ProcessTypeDto>>(entities);
        }

        public static List<ProcessTypeDto> ToDtoList(this IQueryable<ProcessType> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProcessTypeDto>>(entities.ToList()) : new List<ProcessTypeDto>();
        }

        public static ProcessTypePersonalDto ToDto(this ProcessTypePersonal entity)
        {
            return entity == null ? default : _mapper.Map<ProcessTypePersonalDto>(entity);
        }

        public static List<ProcessTypePersonalDto> ToDtoList(this List<ProcessTypePersonal> entities)
        {
            return entities == null || !entities.Any() ? new List<ProcessTypePersonalDto>() : _mapper.Map<List<ProcessTypePersonalDto>>(entities);
        }

        public static List<ProcessTypePersonalDto> ToDtoList(this IQueryable<ProcessTypePersonal> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProcessTypePersonalDto>>(entities.ToList()) : new List<ProcessTypePersonalDto>();
        }

        public static TokenDto ToDto(this Token entity)
        {
            return entity == null ? default : _mapper.Map<TokenDto>(entity);
        }

        public static List<TokenDto> ToDtoList(this List<Token> entities)
        {
            return entities == null || !entities.Any() ? new List<TokenDto>() : _mapper.Map<List<TokenDto>>(entities);
        }

        public static List<TokenDto> ToDtoList(this IQueryable<Token> entities)
        {
            return entities.Any() ? _mapper.Map<List<TokenDto>>(entities.ToList()) : new List<TokenDto>();
        }

        public static TranslationDefDto ToDto(this TranslationDef entity)
        {
            return entity == null ? default : _mapper.Map<TranslationDefDto>(entity);
        }

        public static List<TranslationDefDto> ToDtoList(this List<TranslationDef> entities)
        {
            return entities == null || !entities.Any() ? new List<TranslationDefDto>() : _mapper.Map<List<TranslationDefDto>>(entities);
        }

        public static List<TranslationDefDto> ToDtoList(this IQueryable<TranslationDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<TranslationDefDto>>(entities.ToList()) : new List<TranslationDefDto>();
        }

        // Dbo
        public static AspNetRoleClaimsDto ToDto(this AspNetRoleClaims entity)
        {
            return entity == null ? default : _mapper.Map<AspNetRoleClaimsDto>(entity);
        }

        public static List<AspNetRoleClaimsDto> ToDtoList(this List<AspNetRoleClaims> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetRoleClaimsDto>() : _mapper.Map<List<AspNetRoleClaimsDto>>(entities);
        }

        public static List<AspNetRoleClaimsDto> ToDtoList(this IQueryable<AspNetRoleClaims> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetRoleClaimsDto>>(entities.ToList()) : new List<AspNetRoleClaimsDto>();
        }

        public static AspNetRolesDto ToDto(this AspNetRoles entity)
        {
            return entity == null ? default : _mapper.Map<AspNetRolesDto>(entity);
        }

        public static List<AspNetRolesDto> ToDtoList(this List<AspNetRoles> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetRolesDto>() : _mapper.Map<List<AspNetRolesDto>>(entities);
        }

        public static List<AspNetRolesDto> ToDtoList(this IQueryable<AspNetRoles> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetRolesDto>>(entities.ToList()) : new List<AspNetRolesDto>();
        }

        public static AspNetUserClaimsDto ToDto(this AspNetUserClaims entity)
        {
            return entity == null ? default : _mapper.Map<AspNetUserClaimsDto>(entity);
        }

        public static List<AspNetUserClaimsDto> ToDtoList(this List<AspNetUserClaims> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetUserClaimsDto>() : _mapper.Map<List<AspNetUserClaimsDto>>(entities);
        }

        public static List<AspNetUserClaimsDto> ToDtoList(this IQueryable<AspNetUserClaims> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetUserClaimsDto>>(entities.ToList()) : new List<AspNetUserClaimsDto>();
        }

        public static AspNetUserLoginsDto ToDto(this AspNetUserLogins entity)
        {
            return entity == null ? default : _mapper.Map<AspNetUserLoginsDto>(entity);
        }

        public static List<AspNetUserLoginsDto> ToDtoList(this List<AspNetUserLogins> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetUserLoginsDto>() : _mapper.Map<List<AspNetUserLoginsDto>>(entities);
        }

        public static List<AspNetUserLoginsDto> ToDtoList(this IQueryable<AspNetUserLogins> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetUserLoginsDto>>(entities.ToList()) : new List<AspNetUserLoginsDto>();
        }

        public static AspNetUserRolesDto ToDto(this AspNetUserRoles entity)
        {
            return entity == null ? default : _mapper.Map<AspNetUserRolesDto>(entity);
        }

        public static List<AspNetUserRolesDto> ToDtoList(this List<AspNetUserRoles> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetUserRolesDto>() : _mapper.Map<List<AspNetUserRolesDto>>(entities);
        }

        public static List<AspNetUserRolesDto> ToDtoList(this IQueryable<AspNetUserRoles> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetUserRolesDto>>(entities.ToList()) : new List<AspNetUserRolesDto>();
        }

        public static AspNetUsersDto ToDto(this AspNetUsers entity)
        {
            return entity == null ? default : _mapper.Map<AspNetUsersDto>(entity);
        }

        public static List<AspNetUsersDto> ToDtoList(this List<AspNetUsers> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetUsersDto>() : _mapper.Map<List<AspNetUsersDto>>(entities);
        }

        public static List<AspNetUsersDto> ToDtoList(this IQueryable<AspNetUsers> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetUsersDto>>(entities.ToList()) : new List<AspNetUsersDto>();
        }

        public static AspNetUserTokensDto ToDto(this AspNetUserTokens entity)
        {
            return entity == null ? default : _mapper.Map<AspNetUserTokensDto>(entity);
        }

        public static List<AspNetUserTokensDto> ToDtoList(this List<AspNetUserTokens> entities)
        {
            return entities == null || !entities.Any() ? new List<AspNetUserTokensDto>() : _mapper.Map<List<AspNetUserTokensDto>>(entities);
        }

        public static List<AspNetUserTokensDto> ToDtoList(this IQueryable<AspNetUserTokens> entities)
        {
            return entities.Any() ? _mapper.Map<List<AspNetUserTokensDto>>(entities.ToList()) : new List<AspNetUserTokensDto>();
        }

        public static MenuRoleDto ToDto(this MenuRole entity)
        {
            return entity == null ? default : _mapper.Map<MenuRoleDto>(entity);
        }

        public static List<MenuRoleDto> ToDtoList(this List<MenuRole> entities)
        {
            return entities == null || !entities.Any() ? new List<MenuRoleDto>() : _mapper.Map<List<MenuRoleDto>>(entities);
        }

        public static List<MenuRoleDto> ToDtoList(this IQueryable<MenuRole> entities)
        {
            return entities.Any() ? _mapper.Map<List<MenuRoleDto>>(entities.ToList()) : new List<MenuRoleDto>();
        }

        // Finance
        public static AccountDto ToDto(this Account entity)
        {
            return entity == null ? default : _mapper.Map<AccountDto>(entity);
        }

        public static List<AccountDto> ToDtoList(this List<Account> entities)
        {
            return entities == null || !entities.Any() ? new List<AccountDto>() : _mapper.Map<List<AccountDto>>(entities);
        }

        public static List<AccountDto> ToDtoList(this IQueryable<Account> entities)
        {
            return entities.Any() ? _mapper.Map<List<AccountDto>>(entities.ToList()) : new List<AccountDto>();
        }

        public static AccountAddressDto ToDto(this AccountAddress entity)
        {
            return entity == null ? default : _mapper.Map<AccountAddressDto>(entity);
        }

        public static List<AccountAddressDto> ToDtoList(this List<AccountAddress> entities)
        {
            return entities == null || !entities.Any() ? new List<AccountAddressDto>() : _mapper.Map<List<AccountAddressDto>>(entities);
        }

        public static List<AccountAddressDto> ToDtoList(this IQueryable<AccountAddress> entities)
        {
            return entities.Any() ? _mapper.Map<List<AccountAddressDto>>(entities.ToList()) : new List<AccountAddressDto>();
        }

        public static AccountDocumentDto ToDto(this AccountDocument entity)
        {
            return entity == null ? default : _mapper.Map<AccountDocumentDto>(entity);
        }

        public static List<AccountDocumentDto> ToDtoList(this List<AccountDocument> entities)
        {
            return entities == null || !entities.Any() ? new List<AccountDocumentDto>() : _mapper.Map<List<AccountDocumentDto>>(entities);
        }

        public static List<AccountDocumentDto> ToDtoList(this IQueryable<AccountDocument> entities)
        {
            return entities.Any() ? _mapper.Map<List<AccountDocumentDto>>(entities.ToList()) : new List<AccountDocumentDto>();
        }

        public static AccountTransactionDto ToDto(this AccountTransaction entity)
        {
            return entity == null ? default : _mapper.Map<AccountTransactionDto>(entity);
        }

        public static List<AccountTransactionDto> ToDtoList(this List<AccountTransaction> entities)
        {
            return entities == null || !entities.Any() ? new List<AccountTransactionDto>() : _mapper.Map<List<AccountTransactionDto>>(entities);
        }

        public static List<AccountTransactionDto> ToDtoList(this IQueryable<AccountTransaction> entities)
        {
            return entities.Any() ? _mapper.Map<List<AccountTransactionDto>>(entities.ToList()) : new List<AccountTransactionDto>();
        }

        public static CashRegisterDto ToDto(this CashRegister entity)
        {
            return entity == null ? default : _mapper.Map<CashRegisterDto>(entity);
        }

        public static List<CashRegisterDto> ToDtoList(this List<CashRegister> entities)
        {
            return entities == null || !entities.Any() ? new List<CashRegisterDto>() : _mapper.Map<List<CashRegisterDto>>(entities);
        }

        public static List<CashRegisterDto> ToDtoList(this IQueryable<CashRegister> entities)
        {
            return entities.Any() ? _mapper.Map<List<CashRegisterDto>>(entities.ToList()) : new List<CashRegisterDto>();
        }

        public static CashRegisterPersonalDto ToDto(this CashRegisterPersonal entity)
        {
            return entity == null ? default : _mapper.Map<CashRegisterPersonalDto>(entity);
        }

        public static List<CashRegisterPersonalDto> ToDtoList(this List<CashRegisterPersonal> entities)
        {
            return entities == null || !entities.Any() ? new List<CashRegisterPersonalDto>() : _mapper.Map<List<CashRegisterPersonalDto>>(entities);
        }

        public static List<CashRegisterPersonalDto> ToDtoList(this IQueryable<CashRegisterPersonal> entities)
        {
            return entities.Any() ? _mapper.Map<List<CashRegisterPersonalDto>>(entities.ToList()) : new List<CashRegisterPersonalDto>();
        }

        public static CashTransactionDto ToDto(this CashTransaction entity)
        {
            return entity == null ? default : _mapper.Map<CashTransactionDto>(entity);
        }

        public static List<CashTransactionDto> ToDtoList(this List<CashTransaction> entities)
        {
            return entities == null || !entities.Any() ? new List<CashTransactionDto>() : _mapper.Map<List<CashTransactionDto>>(entities);
        }

        public static List<CashTransactionDto> ToDtoList(this IQueryable<CashTransaction> entities)
        {
            return entities.Any() ? _mapper.Map<List<CashTransactionDto>>(entities.ToList()) : new List<CashTransactionDto>();
        }

        public static CheckNoteDto ToDto(this CheckNote entity)
        {
            return entity == null ? default : _mapper.Map<CheckNoteDto>(entity);
        }

        public static List<CheckNoteDto> ToDtoList(this List<CheckNote> entities)
        {
            return entities == null || !entities.Any() ? new List<CheckNoteDto>() : _mapper.Map<List<CheckNoteDto>>(entities);
        }

        public static List<CheckNoteDto> ToDtoList(this IQueryable<CheckNote> entities)
        {
            return entities.Any() ? _mapper.Map<List<CheckNoteDto>>(entities.ToList()) : new List<CheckNoteDto>();
        }

        public static CheckNoteTransactionDto ToDto(this CheckNoteTransaction entity)
        {
            return entity == null ? default : _mapper.Map<CheckNoteTransactionDto>(entity);
        }

        public static List<CheckNoteTransactionDto> ToDtoList(this List<CheckNoteTransaction> entities)
        {
            return entities == null || !entities.Any() ? new List<CheckNoteTransactionDto>() : _mapper.Map<List<CheckNoteTransactionDto>>(entities);
        }

        public static List<CheckNoteTransactionDto> ToDtoList(this IQueryable<CheckNoteTransaction> entities)
        {
            return entities.Any() ? _mapper.Map<List<CheckNoteTransactionDto>>(entities.ToList()) : new List<CheckNoteTransactionDto>();
        }

        public static CurrencyDto ToDto(this Currency entity)
        {
            return entity == null ? default : _mapper.Map<CurrencyDto>(entity);
        }

        public static List<CurrencyDto> ToDtoList(this List<Currency> entities)
        {
            return entities == null || !entities.Any() ? new List<CurrencyDto>() : _mapper.Map<List<CurrencyDto>>(entities);
        }

        public static List<CurrencyDto> ToDtoList(this IQueryable<Currency> entities)
        {
            return entities.Any() ? _mapper.Map<List<CurrencyDto>>(entities.ToList()) : new List<CurrencyDto>();
        }

        public static CurrencyRateDto ToDto(this CurrencyRate entity)
        {
            return entity == null ? default : _mapper.Map<CurrencyRateDto>(entity);
        }

        public static List<CurrencyRateDto> ToDtoList(this List<CurrencyRate> entities)
        {
            return entities == null || !entities.Any() ? new List<CurrencyRateDto>() : _mapper.Map<List<CurrencyRateDto>>(entities);
        }

        public static List<CurrencyRateDto> ToDtoList(this IQueryable<CurrencyRate> entities)
        {
            return entities.Any() ? _mapper.Map<List<CurrencyRateDto>>(entities.ToList()) : new List<CurrencyRateDto>();
        }

        // HangFire
        public static AggregatedCounterDto ToDto(this AggregatedCounter entity)
        {
            return entity == null ? default : _mapper.Map<AggregatedCounterDto>(entity);
        }

        public static List<AggregatedCounterDto> ToDtoList(this List<AggregatedCounter> entities)
        {
            return entities == null || !entities.Any() ? new List<AggregatedCounterDto>() : _mapper.Map<List<AggregatedCounterDto>>(entities);
        }

        public static List<AggregatedCounterDto> ToDtoList(this IQueryable<AggregatedCounter> entities)
        {
            return entities.Any() ? _mapper.Map<List<AggregatedCounterDto>>(entities.ToList()) : new List<AggregatedCounterDto>();
        }

        public static CounterDto ToDto(this Counter entity)
        {
            return entity == null ? default : _mapper.Map<CounterDto>(entity);
        }

        public static List<CounterDto> ToDtoList(this List<Counter> entities)
        {
            return entities == null || !entities.Any() ? new List<CounterDto>() : _mapper.Map<List<CounterDto>>(entities);
        }

        public static List<CounterDto> ToDtoList(this IQueryable<Counter> entities)
        {
            return entities.Any() ? _mapper.Map<List<CounterDto>>(entities.ToList()) : new List<CounterDto>();
        }

        public static HashDto ToDto(this Hash entity)
        {
            return entity == null ? default : _mapper.Map<HashDto>(entity);
        }

        public static List<HashDto> ToDtoList(this List<Hash> entities)
        {
            return entities == null || !entities.Any() ? new List<HashDto>() : _mapper.Map<List<HashDto>>(entities);
        }

        public static List<HashDto> ToDtoList(this IQueryable<Hash> entities)
        {
            return entities.Any() ? _mapper.Map<List<HashDto>>(entities.ToList()) : new List<HashDto>();
        }

        public static JobDto ToDto(this Job entity)
        {
            return entity == null ? default : _mapper.Map<JobDto>(entity);
        }

        public static List<JobDto> ToDtoList(this List<Job> entities)
        {
            return entities == null || !entities.Any() ? new List<JobDto>() : _mapper.Map<List<JobDto>>(entities);
        }

        public static List<JobDto> ToDtoList(this IQueryable<Job> entities)
        {
            return entities.Any() ? _mapper.Map<List<JobDto>>(entities.ToList()) : new List<JobDto>();
        }

        public static JobParameterDto ToDto(this JobParameter entity)
        {
            return entity == null ? default : _mapper.Map<JobParameterDto>(entity);
        }

        public static List<JobParameterDto> ToDtoList(this List<JobParameter> entities)
        {
            return entities == null || !entities.Any() ? new List<JobParameterDto>() : _mapper.Map<List<JobParameterDto>>(entities);
        }

        public static List<JobParameterDto> ToDtoList(this IQueryable<JobParameter> entities)
        {
            return entities.Any() ? _mapper.Map<List<JobParameterDto>>(entities.ToList()) : new List<JobParameterDto>();
        }

        public static JobQueueDto ToDto(this JobQueue entity)
        {
            return entity == null ? default : _mapper.Map<JobQueueDto>(entity);
        }

        public static List<JobQueueDto> ToDtoList(this List<JobQueue> entities)
        {
            return entities == null || !entities.Any() ? new List<JobQueueDto>() : _mapper.Map<List<JobQueueDto>>(entities);
        }

        public static List<JobQueueDto> ToDtoList(this IQueryable<JobQueue> entities)
        {
            return entities.Any() ? _mapper.Map<List<JobQueueDto>>(entities.ToList()) : new List<JobQueueDto>();
        }

        public static ListDto ToDto(this List entity)
        {
            return entity == null ? default : _mapper.Map<ListDto>(entity);
        }

        public static List<ListDto> ToDtoList(this List<List> entities)
        {
            return entities == null || !entities.Any() ? new List<ListDto>() : _mapper.Map<List<ListDto>>(entities);
        }

        public static List<ListDto> ToDtoList(this IQueryable<List> entities)
        {
            return entities.Any() ? _mapper.Map<List<ListDto>>(entities.ToList()) : new List<ListDto>();
        }

        public static SchemaDto ToDto(this Schema entity)
        {
            return entity == null ? default : _mapper.Map<SchemaDto>(entity);
        }

        public static List<SchemaDto> ToDtoList(this List<Schema> entities)
        {
            return entities == null || !entities.Any() ? new List<SchemaDto>() : _mapper.Map<List<SchemaDto>>(entities);
        }

        public static List<SchemaDto> ToDtoList(this IQueryable<Schema> entities)
        {
            return entities.Any() ? _mapper.Map<List<SchemaDto>>(entities.ToList()) : new List<SchemaDto>();
        }

        public static ServerDto ToDto(this Server entity)
        {
            return entity == null ? default : _mapper.Map<ServerDto>(entity);
        }

        public static List<ServerDto> ToDtoList(this List<Server> entities)
        {
            return entities == null || !entities.Any() ? new List<ServerDto>() : _mapper.Map<List<ServerDto>>(entities);
        }

        public static List<ServerDto> ToDtoList(this IQueryable<Server> entities)
        {
            return entities.Any() ? _mapper.Map<List<ServerDto>>(entities.ToList()) : new List<ServerDto>();
        }

        public static SetDto ToDto(this Set entity)
        {
            return entity == null ? default : _mapper.Map<SetDto>(entity);
        }

        public static List<SetDto> ToDtoList(this List<Set> entities)
        {
            return entities == null || !entities.Any() ? new List<SetDto>() : _mapper.Map<List<SetDto>>(entities);
        }

        public static List<SetDto> ToDtoList(this IQueryable<Set> entities)
        {
            return entities.Any() ? _mapper.Map<List<SetDto>>(entities.ToList()) : new List<SetDto>();
        }

        public static StateDto ToDto(this State entity)
        {
            return entity == null ? default : _mapper.Map<StateDto>(entity);
        }

        public static List<StateDto> ToDtoList(this List<State> entities)
        {
            return entities == null || !entities.Any() ? new List<StateDto>() : _mapper.Map<List<StateDto>>(entities);
        }

        public static List<StateDto> ToDtoList(this IQueryable<State> entities)
        {
            return entities.Any() ? _mapper.Map<List<StateDto>>(entities.ToList()) : new List<StateDto>();
        }

        // Inventory
        public static BrandDto ToDto(this Brand entity)
        {
            return entity == null ? default : _mapper.Map<BrandDto>(entity);
        }

        public static List<BrandDto> ToDtoList(this List<Brand> entities)
        {
            return entities == null || !entities.Any() ? new List<BrandDto>() : _mapper.Map<List<BrandDto>>(entities);
        }

        public static List<BrandDto> ToDtoList(this IQueryable<Brand> entities)
        {
            return entities.Any() ? _mapper.Map<List<BrandDto>>(entities.ToList()) : new List<BrandDto>();
        }

        public static ProductDto ToDto(this Product entity)
        {
            return entity == null ? default : _mapper.Map<ProductDto>(entity);
        }

        public static List<ProductDto> ToDtoList(this List<Product> entities)
        {
            return entities == null || !entities.Any() ? new List<ProductDto>() : _mapper.Map<List<ProductDto>>(entities);
        }

        public static List<ProductDto> ToDtoList(this IQueryable<Product> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProductDto>>(entities.ToList()) : new List<ProductDto>();
        }

        public static ProductBarcodeDto ToDto(this ProductBarcode entity)
        {
            return entity == null ? default : _mapper.Map<ProductBarcodeDto>(entity);
        }

        public static List<ProductBarcodeDto> ToDtoList(this List<ProductBarcode> entities)
        {
            return entities == null || !entities.Any() ? new List<ProductBarcodeDto>() : _mapper.Map<List<ProductBarcodeDto>>(entities);
        }

        public static List<ProductBarcodeDto> ToDtoList(this IQueryable<ProductBarcode> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProductBarcodeDto>>(entities.ToList()) : new List<ProductBarcodeDto>();
        }

        public static ProductColorPaletteDto ToDto(this ProductColorPalette entity)
        {
            return entity == null ? default : _mapper.Map<ProductColorPaletteDto>(entity);
        }

        public static List<ProductColorPaletteDto> ToDtoList(this List<ProductColorPalette> entities)
        {
            return entities == null || !entities.Any() ? new List<ProductColorPaletteDto>() : _mapper.Map<List<ProductColorPaletteDto>>(entities);
        }

        public static List<ProductColorPaletteDto> ToDtoList(this IQueryable<ProductColorPalette> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProductColorPaletteDto>>(entities.ToList()) : new List<ProductColorPaletteDto>();
        }

        public static ProductUnitDto ToDto(this ProductUnit entity)
        {
            return entity == null ? default : _mapper.Map<ProductUnitDto>(entity);
        }

        public static List<ProductUnitDto> ToDtoList(this List<ProductUnit> entities)
        {
            return entities == null || !entities.Any() ? new List<ProductUnitDto>() : _mapper.Map<List<ProductUnitDto>>(entities);
        }

        public static List<ProductUnitDto> ToDtoList(this IQueryable<ProductUnit> entities)
        {
            return entities.Any() ? _mapper.Map<List<ProductUnitDto>>(entities.ToList()) : new List<ProductUnitDto>();
        }

        public static StockCountDto ToDto(this StockCount entity)
        {
            return entity == null ? default : _mapper.Map<StockCountDto>(entity);
        }

        public static List<StockCountDto> ToDtoList(this List<StockCount> entities)
        {
            return entities == null || !entities.Any() ? new List<StockCountDto>() : _mapper.Map<List<StockCountDto>>(entities);
        }

        public static List<StockCountDto> ToDtoList(this IQueryable<StockCount> entities)
        {
            return entities.Any() ? _mapper.Map<List<StockCountDto>>(entities.ToList()) : new List<StockCountDto>();
        }

        public static StockCountLineDto ToDto(this StockCountLine entity)
        {
            return entity == null ? default : _mapper.Map<StockCountLineDto>(entity);
        }

        public static List<StockCountLineDto> ToDtoList(this List<StockCountLine> entities)
        {
            return entities == null || !entities.Any() ? new List<StockCountLineDto>() : _mapper.Map<List<StockCountLineDto>>(entities);
        }

        public static List<StockCountLineDto> ToDtoList(this IQueryable<StockCountLine> entities)
        {
            return entities.Any() ? _mapper.Map<List<StockCountLineDto>>(entities.ToList()) : new List<StockCountLineDto>();
        }

        public static StoreDto ToDto(this Store entity)
        {
            return entity == null ? default : _mapper.Map<StoreDto>(entity);
        }

        public static List<StoreDto> ToDtoList(this List<Store> entities)
        {
            return entities == null || !entities.Any() ? new List<StoreDto>() : _mapper.Map<List<StoreDto>>(entities);
        }

        public static List<StoreDto> ToDtoList(this IQueryable<Store> entities)
        {
            return entities.Any() ? _mapper.Map<List<StoreDto>>(entities.ToList()) : new List<StoreDto>();
        }

        public static StorePersonalDto ToDto(this StorePersonal entity)
        {
            return entity == null ? default : _mapper.Map<StorePersonalDto>(entity);
        }

        public static List<StorePersonalDto> ToDtoList(this List<StorePersonal> entities)
        {
            return entities == null || !entities.Any() ? new List<StorePersonalDto>() : _mapper.Map<List<StorePersonalDto>>(entities);
        }

        public static List<StorePersonalDto> ToDtoList(this IQueryable<StorePersonal> entities)
        {
            return entities.Any() ? _mapper.Map<List<StorePersonalDto>>(entities.ToList()) : new List<StorePersonalDto>();
        }

        public static UnitDto ToDto(this Unit entity)
        {
            return entity == null ? default : _mapper.Map<UnitDto>(entity);
        }

        public static List<UnitDto> ToDtoList(this List<Unit> entities)
        {
            return entities == null || !entities.Any() ? new List<UnitDto>() : _mapper.Map<List<UnitDto>>(entities);
        }

        public static List<UnitDto> ToDtoList(this IQueryable<Unit> entities)
        {
            return entities.Any() ? _mapper.Map<List<UnitDto>>(entities.ToList()) : new List<UnitDto>();
        }

        // Report
        public static ReportDefDto ToDto(this ReportDef entity)
        {
            return entity == null ? default : _mapper.Map<ReportDefDto>(entity);
        }

        public static List<ReportDefDto> ToDtoList(this List<ReportDef> entities)
        {
            return entities == null || !entities.Any() ? new List<ReportDefDto>() : _mapper.Map<List<ReportDefDto>>(entities);
        }

        public static List<ReportDefDto> ToDtoList(this IQueryable<ReportDef> entities)
        {
            return entities.Any() ? _mapper.Map<List<ReportDefDto>>(entities.ToList()) : new List<ReportDefDto>();
        }

        // Trade
        public static TradeDocumentDto ToDto(this TradeDocument entity)
        {
            return entity == null ? default : _mapper.Map<TradeDocumentDto>(entity);
        }

        public static List<TradeDocumentDto> ToDtoList(this List<TradeDocument> entities)
        {
            return entities == null || !entities.Any() ? new List<TradeDocumentDto>() : _mapper.Map<List<TradeDocumentDto>>(entities);
        }

        public static List<TradeDocumentDto> ToDtoList(this IQueryable<TradeDocument> entities)
        {
            return entities.Any() ? _mapper.Map<List<TradeDocumentDto>>(entities.ToList()) : new List<TradeDocumentDto>();
        }

        public static TradeDocumentCurrencyDto ToDto(this TradeDocumentCurrency entity)
        {
            return entity == null ? default : _mapper.Map<TradeDocumentCurrencyDto>(entity);
        }

        public static List<TradeDocumentCurrencyDto> ToDtoList(this List<TradeDocumentCurrency> entities)
        {
            return entities == null || !entities.Any() ? new List<TradeDocumentCurrencyDto>() : _mapper.Map<List<TradeDocumentCurrencyDto>>(entities);
        }

        public static List<TradeDocumentCurrencyDto> ToDtoList(this IQueryable<TradeDocumentCurrency> entities)
        {
            return entities.Any() ? _mapper.Map<List<TradeDocumentCurrencyDto>>(entities.ToList()) : new List<TradeDocumentCurrencyDto>();
        }

        public static TradeDocumentLineDto ToDto(this TradeDocumentLine entity)
        {
            return entity == null ? default : _mapper.Map<TradeDocumentLineDto>(entity);
        }

        public static List<TradeDocumentLineDto> ToDtoList(this List<TradeDocumentLine> entities)
        {
            return entities == null || !entities.Any() ? new List<TradeDocumentLineDto>() : _mapper.Map<List<TradeDocumentLineDto>>(entities);
        }

        public static List<TradeDocumentLineDto> ToDtoList(this IQueryable<TradeDocumentLine> entities)
        {
            return entities.Any() ? _mapper.Map<List<TradeDocumentLineDto>>(entities.ToList()) : new List<TradeDocumentLineDto>();
        }

        public static TradeDocumentLineTempDto ToDto(this TradeDocumentLineTemp entity)
        {
            return entity == null ? default : _mapper.Map<TradeDocumentLineTempDto>(entity);
        }

        public static List<TradeDocumentLineTempDto> ToDtoList(this List<TradeDocumentLineTemp> entities)
        {
            return entities == null || !entities.Any() ? new List<TradeDocumentLineTempDto>() : _mapper.Map<List<TradeDocumentLineTempDto>>(entities);
        }

        public static List<TradeDocumentLineTempDto> ToDtoList(this IQueryable<TradeDocumentLineTemp> entities)
        {
            return entities.Any() ? _mapper.Map<List<TradeDocumentLineTempDto>>(entities.ToList()) : new List<TradeDocumentLineTempDto>();
        }

        public static TradeDocumentTempDto ToDto(this TradeDocumentTemp entity)
        {
            return entity == null ? default : _mapper.Map<TradeDocumentTempDto>(entity);
        }

        public static List<TradeDocumentTempDto> ToDtoList(this List<TradeDocumentTemp> entities)
        {
            return entities == null || !entities.Any() ? new List<TradeDocumentTempDto>() : _mapper.Map<List<TradeDocumentTempDto>>(entities);
        }

        public static List<TradeDocumentTempDto> ToDtoList(this IQueryable<TradeDocumentTemp> entities)
        {
            return entities.Any() ? _mapper.Map<List<TradeDocumentTempDto>>(entities.ToList()) : new List<TradeDocumentTempDto>();
        }

    }
}
