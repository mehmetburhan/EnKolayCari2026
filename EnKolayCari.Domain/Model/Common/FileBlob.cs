using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class FileBlob  //Dosya icerigi (binary). FK -> FileHeader. Eski: DosyalarBlob.
    {
        public long Id { get; set; }  //Birincil anahtar (Identity)
        public long FileHeaderId { get; set; }  //FK: common.FileHeader.Id
        public byte[] Blob { get; set; }  //Binary dosya icerigi. Eski: DosyalarBlob.Blob

        public virtual FileHeader FileHeader { get; set; }
    }
}
