using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.HangFire
{
    public class Job 
    {
        public long Id { get; set; } 
        public long? StateId { get; set; } 
        public string StateName { get; set; } 
        public string InvocationData { get; set; } 
        public string Arguments { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? ExpireAt { get; set; } 
    }
}
