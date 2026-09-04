using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class FileBlob  //Dosya icerigi (binary). FK -> FileHeader. Eski: DosyalarBlob.
    {
        public long Id { get; set; }  //Birincil anahtar (Identity)
        public long FileHeaderId { get; set; }  //FK: common.FileHeader.Id
        public byte[] Blob { get; set; }  //Binary dosya icerigi. Eski: DosyalarBlob.Blob

        public virtual FileHeader FileHeader { get; set; }
    }
}
