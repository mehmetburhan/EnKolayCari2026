using System;

namespace EnKolayCari2026.Application.Model.DTO.HangFire
{
    public class SetDto
    {
        public string Key { get; set; }
        public double Score { get; set; }
        public string Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
