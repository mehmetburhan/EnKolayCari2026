using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.HangFire
{
    public class List 
    {
        public long Id { get; set; } 
        public string Key { get; set; } 
        public string Value { get; set; } 
        public DateTime? ExpireAt { get; set; } 
    }
}
