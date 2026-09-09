using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.HangFire
{
    public class List 
    {
        public long Id { get; set; } 
        public string Key { get; set; } 
        public string Value { get; set; } 
        public DateTime? ExpireAt { get; set; } 
    }
}
