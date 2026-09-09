using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.HangFire
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
