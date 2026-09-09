using System;

namespace EnKolayCari2026.Application.Model.DTO.HangFire
{
    public class HashDto
    {
        public string Key { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
