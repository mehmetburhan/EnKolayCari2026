using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.ToTable("Company", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Company_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.Code)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("Code");

            entity.Property(e => e.City)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("City");

            entity.Property(e => e.IsActivationCompleted)
                .IsRequired(true)
                .HasColumnName("IsActivationCompleted");

            entity.Property(e => e.ActivationDate)
                .IsRequired(false)
                .HasColumnName("ActivationDate");

            entity.Property(e => e.Title)
                .IsRequired(true)
                .HasMaxLength(250)
                .HasColumnName("Title");

            entity.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Email");

            entity.Property(e => e.WebsiteUrl)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("WebsiteUrl");

            entity.Property(e => e.Address)
                .IsRequired(false)
                .HasMaxLength(1000)
                .HasColumnName("Address");

            entity.Property(e => e.District)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("District");

            entity.Property(e => e.MapUrl)
                .IsRequired(false)
                .HasMaxLength(400)
                .HasColumnName("MapUrl");

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

            entity.Property(e => e.AuthorizedPerson)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("AuthorizedPerson");

            entity.Property(e => e.DefaultCurrencyCode)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("DefaultCurrencyCode");

            entity.Property(e => e.CurrencyCodes)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("CurrencyCodes");

            entity.Property(e => e.Parameters)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Parameters");

            entity.Property(e => e.LegacyServerId)
                .IsRequired(false)
                .HasColumnName("LegacyServerId");

            entity.Property(e => e.WeightBarcodeLength)
                .IsRequired(true)
                .HasColumnName("WeightBarcodeLength");

            entity.Property(e => e.WeightBarcodeDecimalLength)
                .IsRequired(true)
                .HasColumnName("WeightBarcodeDecimalLength");

            entity.Property(e => e.Integrator)
                .IsRequired(false)
                .HasColumnName("Integrator");

            entity.Property(e => e.IntegratorUserName)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("IntegratorUserName");

            entity.Property(e => e.IntegratorPassword)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("IntegratorPassword");

            entity.Property(e => e.UserCount)
                .IsRequired(false)
                .HasColumnName("UserCount");

            entity.Property(e => e.AboutUs)
                .IsRequired(false)
                .HasColumnName("AboutUs");

            entity.Property(e => e.ColorSizeLabel)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ColorSizeLabel");

            entity.Property(e => e.EcommerceStyle)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("EcommerceStyle");

            entity.Property(e => e.EcommerceStylePrefix)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("EcommerceStylePrefix");

            entity.Property(e => e.FavIconPrefix)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("FavIconPrefix");

            entity.Property(e => e.EcommerceStyle1)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("EcommerceStyle1");

            entity.Property(e => e.EcommerceStylePrefix1)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("EcommerceStylePrefix1");

            entity.Property(e => e.FavIconPrefix1)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("FavIconPrefix1");

            entity.Property(e => e.FreeShippingLimit)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("FreeShippingLimit");

            entity.Property(e => e.WeightServiceFee)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("WeightServiceFee");

            entity.Property(e => e.DesiServiceFee)
                .IsRequired(false)
                .HasPrecision(18,4)
                .HasColumnName("DesiServiceFee");

            entity.Property(e => e.ShippingFee)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("ShippingFee");

            entity.Property(e => e.ShippingLabel)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ShippingLabel");

            entity.Property(e => e.FacebookUrl)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("FacebookUrl");

            entity.Property(e => e.InstagramUrl)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("InstagramUrl");

            entity.Property(e => e.TwitterUrl)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("TwitterUrl");

            entity.Property(e => e.PinterestUrl)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("PinterestUrl");

            entity.Property(e => e.YoutubeUrl)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("YoutubeUrl");

            entity.Property(e => e.IsBankTransferActive)
                .IsRequired(false)
                .HasColumnName("IsBankTransferActive");

            entity.Property(e => e.IsCashOnDeliveryActive)
                .IsRequired(false)
                .HasColumnName("IsCashOnDeliveryActive");

            entity.Property(e => e.EcommerceSiteName)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("EcommerceSiteName");

            entity.Property(e => e.ShowcaseName)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("ShowcaseName");

            entity.Property(e => e.ShowcaseLabel)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ShowcaseLabel");

            entity.Property(e => e.GoogleAnalyticsId)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("GoogleAnalyticsId");

            entity.Property(e => e.FacebookPixelId)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("FacebookPixelId");

            entity.Property(e => e.WhatsAppPhone)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("WhatsAppPhone");

            entity.Property(e => e.IsSslActive)
                .IsRequired(true)
                .HasColumnName("IsSslActive");

            entity.Property(e => e.IsWideTopMenuActive)
                .IsRequired(true)
                .HasColumnName("IsWideTopMenuActive");

            entity.Property(e => e.AllProductsMainMenuActive)
                .IsRequired(true)
                .HasColumnName("AllProductsMainMenuActive");

            entity.Property(e => e.AllProductsSubMenuActive)
                .IsRequired(true)
                .HasColumnName("AllProductsSubMenuActive");

            entity.Property(e => e.ProductImageWidth)
                .IsRequired(true)
                .HasColumnName("ProductImageWidth");

            entity.Property(e => e.ProductImageHeight)
                .IsRequired(true)
                .HasColumnName("ProductImageHeight");

            entity.Property(e => e.SeoKeywords)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("SeoKeywords");

            entity.Property(e => e.GenerateSitemap)
                .IsRequired(true)
                .HasColumnName("GenerateSitemap");

            entity.Property(e => e.HideCategoryText)
                .IsRequired(true)
                .HasColumnName("HideCategoryText");

            entity.Property(e => e.ShowAllStores)
                .IsRequired(true)
                .HasColumnName("ShowAllStores");

            entity.Property(e => e.ShowAllCashRegisters)
                .IsRequired(true)
                .HasColumnName("ShowAllCashRegisters");

            entity.Property(e => e.TrendyolSellerId)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("TrendyolSellerId");

            entity.Property(e => e.TrendyolApiKey)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("TrendyolApiKey");

            entity.Property(e => e.TrendyolApiSecret)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("TrendyolApiSecret");

            entity.Property(e => e.ShowColorSizeOnProductScreen)
                .IsRequired(true)
                .HasColumnName("ShowColorSizeOnProductScreen");

            entity.Property(e => e.FirstPurchaseDiscountPercent)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("FirstPurchaseDiscountPercent");

            entity.Property(e => e.UseOwnMailSettings)
                .IsRequired(false)
                .HasColumnName("UseOwnMailSettings");

            entity.Property(e => e.MailAddress)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("MailAddress");

            entity.Property(e => e.MailDisplayName)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("MailDisplayName");

            entity.Property(e => e.MailUserName)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("MailUserName");

            entity.Property(e => e.MailPassword)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("MailPassword");

            entity.Property(e => e.MailHost)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("MailHost");

            entity.Property(e => e.MailPort)
                .IsRequired(false)
                .HasColumnName("MailPort");

            entity.Property(e => e.IsDynamicExchangeRate)
                .IsRequired(false)
                .HasColumnName("IsDynamicExchangeRate");

            entity.Property(e => e.LogoHeight)
                .IsRequired(true)
                .HasColumnName("LogoHeight");

            entity.Property(e => e.MersisNumber)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("MersisNumber");

            entity.Property(e => e.TradeRegistryNumber)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TradeRegistryNumber");

            entity.Property(e => e.ImportExternalData)
                .IsRequired(true)
                .HasColumnName("ImportExternalData");

            entity.Property(e => e.ExternalSystemName)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("ExternalSystemName");

            entity.Property(e => e.ExternalSystemIp)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ExternalSystemIp");

            entity.Property(e => e.ExternalSystemDb)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ExternalSystemDb");

            entity.Property(e => e.ExternalSystemUser)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ExternalSystemUser");

            entity.Property(e => e.ExternalSystemPassword)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ExternalSystemPassword");

            entity.Property(e => e.IntegratorEInvoiceTemplate)
                .IsRequired(false)
                .HasColumnName("IntegratorEInvoiceTemplate");

            entity.Property(e => e.IntegratorEArchiveTemplate)
                .IsRequired(false)
                .HasColumnName("IntegratorEArchiveTemplate");

            entity.Property(e => e.SmsPhoneNumber)
                .IsRequired(false)
                .HasMaxLength(20)
                .HasColumnName("SmsPhoneNumber");

            entity.Property(e => e.SmsTitle)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("SmsTitle");

            entity.Property(e => e.SmsUserName)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("SmsUserName");

            entity.Property(e => e.SmsPassword)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("SmsPassword");

            entity.Property(e => e.AppointmentFirstSms)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("AppointmentFirstSms");

            entity.Property(e => e.AppointmentChangeSms)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("AppointmentChangeSms");

            entity.Property(e => e.AppointmentCancelForceSms)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("AppointmentCancelForceSms");

            entity.Property(e => e.AppointmentCancelPatientSms)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("AppointmentCancelPatientSms");

            entity.Property(e => e.AppCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("AppCode");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Company> entity);
    }
}
