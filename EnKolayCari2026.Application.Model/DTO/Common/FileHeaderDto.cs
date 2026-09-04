using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class FileHeaderDto
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public long? TableId { get; set; }
        public string FileName { get; set; }
        public int FileOrder { get; set; }
        public string FileType { get; set; }
    }
}
