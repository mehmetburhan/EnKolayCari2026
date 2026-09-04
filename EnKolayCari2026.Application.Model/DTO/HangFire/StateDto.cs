using System;

namespace EnKolayCari2026.Application.Model.DTO.HangFire
{
    public class StateDto
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Data { get; set; }
    }
}
