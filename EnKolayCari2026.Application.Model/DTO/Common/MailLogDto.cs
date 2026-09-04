using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class MailLogDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string TableName { get; set; }
        public long TableId { get; set; }
        public string MailTypeCode { get; set; }
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public DateTime SentDate { get; set; }
        public long? SentByPersonalId { get; set; }
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
