using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class PersonalLoginActivityDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long PersonalId { get; set; }
        public string ActivityType { get; set; }
        public bool IsSuccess { get; set; }
        public string CustomHeaderJson { get; set; }
        public string DeviceId { get; set; }
        public string ClientOs { get; set; }
        public string AppVersion { get; set; }
        public string Language { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
