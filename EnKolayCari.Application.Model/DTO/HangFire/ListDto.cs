using System;

namespace EnKolayCari.Application.Model.DTO.HangFire
{
    public class ListDto
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
