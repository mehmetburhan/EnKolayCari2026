using System;

namespace EnKolayCari.Application.Model.DTO.HangFire
{
    public class CounterDto
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
        public long Id { get; set; }
    }
}
