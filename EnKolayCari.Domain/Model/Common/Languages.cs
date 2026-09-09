using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class Languages  //AI platform tablosu. Desteklenen UI/servis dilleri. Min tr-TR + en-US (AI-02i). PK = LanguageCode.
    {
        public bool Stat { get; set; }  //Dil aktif mi (AcceptLanguages ile uyumlu).
        public bool MasterLanguge { get; set; }  //Master dil mi (yazim: MasterLanguge — mevcut sozlesme).
        public string LanguageCode { get; set; }  //Kultur kodu PK (tr-TR, en-US...).
        public string DisLanguageCode { get; set; }  //Kisa gosterim kodu (TR, EN...).
        public string Description { get; set; }  //Dil adi (gorunen).
        public int Order { get; set; }  //Siralama.
        public string Flag { get; set; }  //Bayrak ikon yolu (UI).
    }
}
