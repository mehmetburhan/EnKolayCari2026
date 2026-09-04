using System;

namespace EnKolayCari2026.Application.Model.DTO.HangFire
{
    public class AggregatedCounterDto
    {
        public string Key { get; set; }
        public long Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
