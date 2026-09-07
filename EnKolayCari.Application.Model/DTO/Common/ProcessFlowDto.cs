using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class ProcessFlowDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long ModulId { get; set; }
        public string FromCode { get; set; }
        public string ToCode { get; set; }
    }
}
