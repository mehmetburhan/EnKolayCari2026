using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class LicenseType  //Eski tablo: LisansTipi (TICARI_MASTER). Yeni şema: common.LicenseType
    {
        public string Code { get; set; }  //Paket kodu (PK). | Eski alan: LisansTipi.Kod
        public int Value { get; set; }  //Paket oncelik degeri (bitmask). | Eski alan: LisansTipi.Deger
        public bool? ProductTracking { get; set; }  //Urun/stok modulu dahil mi? | Eski alan: LisansTipi.UrunTakip
        public bool? AccountTracking { get; set; }  //Cari modulu dahil mi? | Eski alan: LisansTipi.CariTakip
        public bool? CheckNoteTracking { get; set; }  //Cek/senet modulu dahil mi? | Eski alan: LisansTipi.CekSenetTakip
        public bool? TradeDocumentTracking { get; set; }  //Fatura modulu dahil mi? | Eski alan: LisansTipi.FaturaTakip
        public bool? PaymentTracking { get; set; }  //Odeme modulu dahil mi? | Eski alan: LisansTipi.OdemeTakip
        public bool? BankTracking { get; set; }  //Banka modulu dahil mi? | Eski alan: LisansTipi.BankaTakip
        public bool? ShippingNoteTracking { get; set; }  //Irsaliye modulu dahil mi? | Eski alan: LisansTipi.IrsaliyeTakip
        public bool? QuoteOrderTracking { get; set; }  //Teklif/siparis modulu dahil mi? | Eski alan: LisansTipi.TeklifSiparisTakip
        public bool? EArchiveEInvoice { get; set; }  //e-Arsiv/e-Fatura modulu dahil mi? | Eski alan: LisansTipi.EArsivEFatura
        public bool? MedicalService { get; set; }  //Servis modulu dahil mi? | Eski alan: LisansTipi.Servis
        public bool? IsEcommerce { get; set; }  //E-ticaret modulu dahil mi? | Eski alan: LisansTipi.ETicaret
    }
}
