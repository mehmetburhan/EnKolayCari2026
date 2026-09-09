using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
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
