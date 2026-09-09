using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class ProcessTypeDto
    {
        public long Id { get; set; }
        public long ModulId { get; set; }
        public string Code { get; set; }
        public string DescriptionTR { get; set; }
        public string DescriptionEN { get; set; }
        public int Ordered { get; set; }
        public string Color { get; set; }
    }
}
