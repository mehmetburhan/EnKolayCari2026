using System;

namespace EnKolayCari2026.Application.Model.DTO.HangFire
{
    public class CounterDto
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
        public long Id { get; set; }
    }
}
