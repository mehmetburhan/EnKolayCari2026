using System;

namespace EnKolayCari.Application.Model.DTO.HangFire
{
    public class JobQueueDto
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string Queue { get; set; }
        public DateTime? FetchedAt { get; set; }
    }
}
