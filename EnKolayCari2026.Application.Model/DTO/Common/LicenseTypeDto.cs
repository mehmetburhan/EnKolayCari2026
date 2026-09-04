using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class LicenseTypeDto
    {
        public string Code { get; set; }
        public int Value { get; set; }
        public bool? ProductTakip { get; set; }
        public bool? AccountTakip { get; set; }
        public bool? CheckNoteTakip { get; set; }
        public bool? TradeDocumentTakip { get; set; }
        public bool? PaymentTracking { get; set; }
        public bool? BankTracking { get; set; }
        public bool? IrsaliyeTakip { get; set; }
        public bool? TeklifSiparisTakip { get; set; }
        public bool? EArchiveEInvoice { get; set; }
        public bool? MedicalService { get; set; }
        public bool? IsEcommerce { get; set; }
    }
}
