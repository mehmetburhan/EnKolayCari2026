using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.HangFire
{
    public class Counter 
    {
        public string Key { get; set; } 
        public int Value { get; set; } 
        public DateTime? ExpireAt { get; set; } 
        public long Id { get; set; } 
    }
}
