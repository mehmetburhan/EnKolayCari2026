using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
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
