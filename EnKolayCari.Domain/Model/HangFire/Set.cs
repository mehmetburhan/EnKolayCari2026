using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.HangFire
{
    public class Set 
    {
        public string Key { get; set; } 
        public double Score { get; set; } 
        public string Value { get; set; } 
        public DateTime? ExpireAt { get; set; } 
    }
}
