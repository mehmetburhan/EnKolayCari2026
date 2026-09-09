using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class LegacyTransferMap  //Transfer esleme tablosu. Eski tablo/GId ile yeni tablo/GId/CompanyId arasini tutar. Veri aktarimi sirasinda kullanilir.
    {
        public long Id { get; set; }  //Birincil anahtar (Identity)
        public Guid GId { get; set; }  //Kuresel benzersiz kimlik
        public long CompanyId { get; set; }  //Yeni kaydin CompanyId degeri (tenant)
        public string LegacyTableName { get; set; }  //Eski tablo adi (ornek: CariTanim, UrunTanim)
        public Guid LegacyGId { get; set; }  //Eski kayit GId
        public DateTime? LegacyLastChangeDate { get; set; }  //Eski kayittaki Insert/Update/Delete tarihlerinden en guncel olan
        public string NewTableName { get; set; }  //Yeni tablo adi (ornek: finance.Account veya Account)
        public Guid NewGId { get; set; }  //Yeni kayit GId
        public DateTime InsertDateTime { get; set; }  //Esleme kaydinin olusturulma zamani
    }
}
