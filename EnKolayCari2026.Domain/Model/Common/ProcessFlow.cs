using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class ProcessFlow  //AI platform tablosu. Durum gecisleri (FromCode → ToCode) per ModulId.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long ModulId { get; set; }  //FK → Moduls.Id.
        public string FromCode { get; set; }  //Kaynak durum kodu.
        public string ToCode { get; set; }  //Hedef durum kodu.

        public virtual Moduls Moduls { get; set; }
    }
}
