using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.HangFire
{
    public class State 
    {
        public long Id { get; set; } 
        public long JobId { get; set; } 
        public string Name { get; set; } 
        public string Reason { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public string Data { get; set; } 
    }
}
