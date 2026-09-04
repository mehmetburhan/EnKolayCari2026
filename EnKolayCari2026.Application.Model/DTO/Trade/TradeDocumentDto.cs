using System;

namespace EnKolayCari2026.Application.Model.DTO.Trade
{
    public class TradeDocumentDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long StoreId { get; set; }
        public long? StoreTanim1Id { get; set; }
        public long AccountId { get; set; }
        public int HareketType { get; set; }
        public string TradeDocumentNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? Saat { get; set; }
        public DateTime? DueDate { get; set; }
        public string CurrencyCode { get; set; }
        public int? SiparisStatus { get; set; }
        public string OrderShippingSlipNumber { get; set; }
        public long? ShippingFirmaDefId { get; set; }
        public string TradeDocumentTeslimAdSoyadTitle { get; set; }
        public string TradeDocumentTeslimEMail { get; set; }
        public string TradeDocumentTeslimPhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public bool DeliverToDifferentAddress { get; set; }
        public string DeliveryFullNameOrTitle { get; set; }
        public string DeliveryEmail { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string TeslimatIl { get; set; }
        public string DeliveryDistrict { get; set; }
        public string DeliveryPostalCode { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public long? RelatedTradeDocumentId { get; set; }
        public string MedicalServiceProductBrandModel { get; set; }
        public string MedicalServiceCihazSeriNo { get; set; }
        public string MedicalServiceSellerCompany { get; set; }
        public string MedicalServiceAksesuar { get; set; }
        public string ServiceDeviceDescription { get; set; }
        public string MedicalServiceMusteriNotu { get; set; }
        public string ServicePersonalNote { get; set; }
        public bool? MedicalServiceGarantiBilgisi { get; set; }
        public int? MedicalServiceStatus { get; set; }
        public DateTime? ServiceDeliveryDate { get; set; }
        public string MedicalServiceTeslimAlanKisi { get; set; }
        public int? TeklifStatus { get; set; }
        public string Label { get; set; }
        public bool DocumentKapali { get; set; }
        public DateTime? ED_SonIslemDate { get; set; }
        public int? ED_Code { get; set; }
        public string ED_Description { get; set; }
        public string ED_DetailDescription { get; set; }
        public int? RecordSource { get; set; }
        public int? EcommercePaymentMethod { get; set; }
        public string EcommercePaymentBank { get; set; }
        public string EcommercePaymentId { get; set; }
        public string IntegrationId { get; set; }
        public string IntegrationName { get; set; }
        public int LineCount { get; set; }
        public decimal ToplamVatTutar { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
        public bool IsDocumentCancelled { get; set; }
        public int? ElectronicDocumentType { get; set; }
        public string ElektronikBelgeNo { get; set; }
        public DateTime? ElectronicDocumentSentDate { get; set; }
        public string ElektronikBelgeHatalari { get; set; }
        public int? ElektronikBelgeGonderimStatus { get; set; }
    }
}
