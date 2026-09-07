using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.HangFire
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
