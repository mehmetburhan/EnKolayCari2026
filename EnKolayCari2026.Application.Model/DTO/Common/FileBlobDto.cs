using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class FileBlobDto
    {
        public long Id { get; set; }
        public long FileHeaderId { get; set; }
        public byte[] Blob { get; set; }
    }
}
