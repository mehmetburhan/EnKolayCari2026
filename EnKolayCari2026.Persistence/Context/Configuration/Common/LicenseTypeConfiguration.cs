using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
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

            entity.Property(e => e.ProductTracking)
                .IsRequired(false)
                .HasColumnName("ProductTracking");

            entity.Property(e => e.AccountTracking)
                .IsRequired(false)
                .HasColumnName("AccountTracking");

            entity.Property(e => e.CheckNoteTracking)
                .IsRequired(false)
                .HasColumnName("CheckNoteTracking");

            entity.Property(e => e.TradeDocumentTracking)
                .IsRequired(false)
                .HasColumnName("TradeDocumentTracking");

            entity.Property(e => e.PaymentTracking)
                .IsRequired(false)
                .HasColumnName("PaymentTracking");

            entity.Property(e => e.BankTracking)
                .IsRequired(false)
                .HasColumnName("BankTracking");

            entity.Property(e => e.ShippingNoteTracking)
                .IsRequired(false)
                .HasColumnName("ShippingNoteTracking");

            entity.Property(e => e.QuoteOrderTracking)
                .IsRequired(false)
                .HasColumnName("QuoteOrderTracking");

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
