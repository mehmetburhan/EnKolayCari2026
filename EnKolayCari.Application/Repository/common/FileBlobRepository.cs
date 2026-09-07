using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Application.Repository.Common
{
    public class FileBlobRepository : BaseRepository<FileBlob>, IFileBlobRepository
    {
        public FileBlobRepository(EnKolayCariContext context) : base(context) { }
    }
}
