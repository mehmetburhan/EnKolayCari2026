using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class FileHeader  //Dosya ust bilgisi (AI mimari). TableName + TableId ile baglanir. Eski: Dosyalar.
    {
        public long Id { get; set; }  //Birincil anahtar (Identity)
        public string TableName { get; set; }  //Sahip tablo adi (ornek: Product, Account, TradeDocument). Eski: Dosyalar.TabloAdi
        public long? TableId { get; set; }  //Sahip kayit Id. Eski: Dosyalar.TabloId
        public string FileName { get; set; }  //Dosya adi. Eski: Dosyalar.DosyaAdi
        public int FileOrder { get; set; }  //Goruntuleme sirasi. Eski: Dosyalar.Sira
        public string FileType { get; set; }  //Dosya tipi (png / jpg / pdf vb.). Eski: Dosyalar.DosyaUzantisi
        public virtual ICollection<FileBlob> FileBlob { get; set; } = new List<FileBlob>();
    }
}
