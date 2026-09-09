using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.HangFire
{
    public class JobQueue 
    {
        public long Id { get; set; } 
        public long JobId { get; set; } 
        public string Queue { get; set; } 
        public DateTime? FetchedAt { get; set; } 
    }
}
