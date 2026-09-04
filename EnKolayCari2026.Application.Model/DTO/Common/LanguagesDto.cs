using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class LanguagesDto
    {
        public bool Stat { get; set; }
        public bool MasterLanguge { get; set; }
        public string LanguageCode { get; set; }
        public string DisLanguageCode { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string Flag { get; set; }
    }
}
