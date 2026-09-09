using System;

namespace EnKolayCari2026.Application.Model.DTO.HangFire
{
    public class ServerDto
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public DateTime LastHeartbeat { get; set; }
    }
}
