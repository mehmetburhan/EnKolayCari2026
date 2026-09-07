using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseType>
    {
        public void Configure(EntityTypeBuilder<LicenseType> entity)
        {
            entity.ToTable("LicenseType", "common");

            entity.Property(e => e.Code)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("Code");

            entity.Property(e => e.Value)
                .IsRequired(true)
                .HasColumnName("Value");

            entity.Property(e => e.ProductTakip)
                .IsRequired(false)
                .HasColumnName("ProductTakip");

            entity.Property(e => e.AccountTakip)
                .IsRequired(false)
                .HasColumnName("AccountTakip");

            entity.Property(e => e.CheckNoteTakip)
                .IsRequired(false)
                .HasColumnName("CheckNoteTakip");

            entity.Property(e => e.TradeDocumentTakip)
                .IsRequired(false)
                .HasColumnName("TradeDocumentTakip");

            entity.Property(e => e.PaymentTracking)
                .IsRequired(false)
                .HasColumnName("PaymentTracking");

            entity.Property(e => e.BankTracking)
                .IsRequired(false)
                .HasColumnName("BankTracking");

            entity.Property(e => e.IrsaliyeTakip)
                .IsRequired(false)
                .HasColumnName("IrsaliyeTakip");

            entity.Property(e => e.TeklifSiparisTakip)
                .IsRequired(false)
                .HasColumnName("TeklifSiparisTakip");

            entity.Property(e => e.EArchiveEInvoice)
                .IsRequired(false)
                .HasColumnName("EArchiveEInvoice");

            entity.Property(e => e.MedicalService)
                .IsRequired(false)
                .HasColumnName("MedicalService");

            entity.Property(e => e.IsEcommerce)
                .IsRequired(false)
                .HasColumnName("IsEcommerce");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<LicenseType> entity);
    }
}
