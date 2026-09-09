using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Persistence.Context.Configuration.Trade
{
    public partial class TradeDocumentConfiguration : IEntityTypeConfiguration<TradeDocument>
    {
        public void Configure(EntityTypeBuilder<TradeDocument> entity)
        {
            entity.ToTable("TradeDocument", "trade");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("TradeDocument_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.StoreId)
                .IsRequired(true)
                .HasColumnName("StoreId");

            entity.Property(e => e.SecondaryStoreId)
                .IsRequired(false)
                .HasColumnName("SecondaryStoreId");

            entity.Property(e => e.AccountId)
                .IsRequired(true)
                .HasColumnName("AccountId");

            entity.Property(e => e.DocumentType)
                .IsRequired(true)
                .HasColumnName("DocumentType");

            entity.Property(e => e.TradeDocumentNo)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("TradeDocumentNo");

            entity.Property(e => e.TransactionDate)
                .IsRequired(true)
                .HasColumnName("TransactionDate");

            entity.Property(e => e.TransactionTime)
                .IsRequired(false)
                .HasColumnName("TransactionTime");

            entity.Property(e => e.DueDate)
                .IsRequired(false)
                .HasColumnName("DueDate");

            entity.Property(e => e.CurrencyCode)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("CurrencyCode");

            entity.Property(e => e.OrderStatus)
                .IsRequired(false)
                .HasColumnName("OrderStatus");

            entity.Property(e => e.OrderShippingSlipNumber)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("OrderShippingSlipNumber");

            entity.Property(e => e.ShippingCompanyDefId)
                .IsRequired(false)
                .HasColumnName("ShippingCompanyDefId");

            entity.Property(e => e.BillingFullNameOrTitle)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("BillingFullNameOrTitle");

            entity.Property(e => e.BillingEmail)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("BillingEmail");

            entity.Property(e => e.BillingPhone)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("BillingPhone");

            entity.Property(e => e.Address)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Address");

            entity.Property(e => e.City)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("City");

            entity.Property(e => e.District)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("District");

            entity.Property(e => e.PostalCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("PostalCode");

            entity.Property(e => e.DeliverToDifferentAddress)
                .IsRequired(true)
                .HasColumnName("DeliverToDifferentAddress");

            entity.Property(e => e.DeliveryFullNameOrTitle)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("DeliveryFullNameOrTitle");

            entity.Property(e => e.DeliveryEmail)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("DeliveryEmail");

            entity.Property(e => e.DeliveryPhone)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("DeliveryPhone");

            entity.Property(e => e.DeliveryAddress)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("DeliveryAddress");

            entity.Property(e => e.DeliveryCity)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("DeliveryCity");

            entity.Property(e => e.DeliveryDistrict)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("DeliveryDistrict");

            entity.Property(e => e.DeliveryPostalCode)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("DeliveryPostalCode");

            entity.Property(e => e.TaxOffice)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TaxOffice");

            entity.Property(e => e.TaxNumber)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TaxNumber");

            entity.Property(e => e.RelatedTradeDocumentId)
                .IsRequired(false)
                .HasColumnName("RelatedTradeDocumentId");

            entity.Property(e => e.MedicalServiceProductBrandModel)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("MedicalServiceProductBrandModel");

            entity.Property(e => e.ServiceDeviceSerialNo)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("ServiceDeviceSerialNo");

            entity.Property(e => e.MedicalServiceSellerCompany)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("MedicalServiceSellerCompany");

            entity.Property(e => e.ServiceAccessories)
                .IsRequired(false)
                .HasMaxLength(750)
                .HasColumnName("ServiceAccessories");

            entity.Property(e => e.ServiceDeviceDescription)
                .IsRequired(false)
                .HasMaxLength(750)
                .HasColumnName("ServiceDeviceDescription");

            entity.Property(e => e.ServiceCustomerNote)
                .IsRequired(false)
                .HasMaxLength(750)
                .HasColumnName("ServiceCustomerNote");

            entity.Property(e => e.ServicePersonalNote)
                .IsRequired(false)
                .HasMaxLength(750)
                .HasColumnName("ServicePersonalNote");

            entity.Property(e => e.ServiceWarrantyInfo)
                .IsRequired(false)
                .HasColumnName("ServiceWarrantyInfo");

            entity.Property(e => e.MedicalServiceStatus)
                .IsRequired(false)
                .HasColumnName("MedicalServiceStatus");

            entity.Property(e => e.ServiceDeliveryDate)
                .IsRequired(false)
                .HasColumnName("ServiceDeliveryDate");

            entity.Property(e => e.ServiceDeliveryRecipient)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("ServiceDeliveryRecipient");

            entity.Property(e => e.QuoteStatus)
                .IsRequired(false)
                .HasColumnName("QuoteStatus");

            entity.Property(e => e.Label)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("Label");

            entity.Property(e => e.IsDocumentClosed)
                .IsRequired(true)
                .HasColumnName("IsDocumentClosed");

            entity.Property(e => e.ED_LastProcessDate)
                .IsRequired(false)
                .HasColumnName("ED_LastProcessDate");

            entity.Property(e => e.ED_Code)
                .IsRequired(false)
                .HasColumnName("ED_Code");

            entity.Property(e => e.ED_Description)
                .IsRequired(false)
                .HasMaxLength(200)
                .HasColumnName("ED_Description");

            entity.Property(e => e.ED_DetailDescription)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("ED_DetailDescription");

            entity.Property(e => e.RecordSource)
                .IsRequired(false)
                .HasColumnName("RecordSource");

            entity.Property(e => e.EcommercePaymentMethod)
                .IsRequired(false)
                .HasColumnName("EcommercePaymentMethod");

            entity.Property(e => e.EcommercePaymentBank)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("EcommercePaymentBank");

            entity.Property(e => e.EcommercePaymentId)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("EcommercePaymentId");

            entity.Property(e => e.IntegrationId)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("IntegrationId");

            entity.Property(e => e.IntegrationName)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("IntegrationName");

            entity.Property(e => e.LineCount)
                .IsRequired(true)
                .HasColumnName("LineCount");

            entity.Property(e => e.TotalVatAmount)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("TotalVatAmount");

            entity.Property(e => e.TotalAmount)
                .IsRequired(true)
                .HasPrecision(18,4)
                .HasColumnName("TotalAmount");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.InsertUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            entity.Property(e => e.IsDocumentCancelled)
                .IsRequired(true)
                .HasColumnName("IsDocumentCancelled");

            entity.Property(e => e.ElectronicDocumentType)
                .IsRequired(false)
                .HasColumnName("ElectronicDocumentType");

            entity.Property(e => e.ElectronicDocumentNo)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("ElectronicDocumentNo");

            entity.Property(e => e.ElectronicDocumentSentDate)
                .IsRequired(false)
                .HasColumnName("ElectronicDocumentSentDate");

            entity.Property(e => e.ElectronicDocumentErrors)
                .IsRequired(false)
                .HasColumnName("ElectronicDocumentErrors");

            entity.Property(e => e.ElectronicDocumentSendStatus)
                .IsRequired(false)
                .HasColumnName("ElectronicDocumentSendStatus");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.TradeDocument)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_TradeDocument_CompanyId");

            entity.HasOne(d => d.Store)
                  .WithMany(p => p.TradeDocument)
                  .HasForeignKey(d => d.StoreId)
                  .HasConstraintName("FK_TradeDocument_StoreId");

            entity.HasOne(d => d.Account)
                  .WithMany(p => p.TradeDocument)
                  .HasForeignKey(d => d.AccountId)
                  .HasConstraintName("FK_TradeDocument_AccountId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TradeDocument> entity);
    }
}
