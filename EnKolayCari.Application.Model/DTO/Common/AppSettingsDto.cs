using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class AppSettingsDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public string AppCode { get; set; }
        public int MaxStayInsideHour { get; set; }
        public string IOSAppUrl { get; set; }
        public string AndroidAppUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public bool IsDelete { get; set; }
    }
}
