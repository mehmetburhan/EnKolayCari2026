using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class Token  //Eski tablo: Token (TICARI_MASTER). Yeni şema: common.Token
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: Token.Id
        public Guid GId { get; set; }  //Token GUID. | Eski alan: Token.GId
        public long CompanyId { get; set; }  //Token sahibi sirket. | Eski alan: Token.SirketTanimId
        public long PersonalId { get; set; }  //Token sahibi kullanici. | Eski alan: Token.PersonelTanimId
        public DateTime ExpreDate { get; set; }  //Token gecerlilik bitis tarihi. | Eski alan: Token.ExpreDate
        public string Application { get; set; }  //Token olusturan uygulama (Web, API, Mobil). | Eski alan: Token.Application
        public DateTime UpdateDateTime { get; set; }  //Son aktivite zamani. | Eski alan: Token.UpdateDateTime

        public virtual Company Company { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
