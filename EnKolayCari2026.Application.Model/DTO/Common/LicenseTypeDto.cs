using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class LicenseTypeDto
    {
        public string Code { get; set; }
        public int Value { get; set; }
        public bool? ProductTracking { get; set; }
        public bool? AccountTracking { get; set; }
        public bool? CheckNoteTracking { get; set; }
        public bool? TradeDocumentTracking { get; set; }
        public bool? PaymentTracking { get; set; }
        public bool? BankTracking { get; set; }
        public bool? ShippingNoteTracking { get; set; }
        public bool? QuoteOrderTracking { get; set; }
        public bool? EArchiveEInvoice { get; set; }
        public bool? MedicalService { get; set; }
        public bool? IsEcommerce { get; set; }
    }
}
