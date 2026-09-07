using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class PersonalWidgetDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public bool Stat { get; set; }
        public long PersonalId { get; set; }
        public string WidgetId { get; set; }
        public string With { get; set; }
        public bool IsVisible { get; set; }
        public int Position { get; set; }
        public int RowIndex { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
