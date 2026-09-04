using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class AppExceptionLogDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long? CompanyId { get; set; }
        public long? PersonalId { get; set; }
        public string DeviceId { get; set; }
        public string ClientOs { get; set; }
        public string AppVersion { get; set; }
        public string Language { get; set; }
        public string IpAddress { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string PageOrScreen { get; set; }
        public string ActionOrMethod { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string SeverityLevel { get; set; }
        public string AdditionalData { get; set; }
        public DateTime OccurredAt { get; set; }
        public bool Stat { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public bool IsDelete { get; set; }
    }
}
