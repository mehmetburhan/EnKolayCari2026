using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Finance;

namespace EnKolayCari.Application.Repository.Finance
{
    public class CheckNoteTransactionRepository : BaseRepository<CheckNoteTransaction>, ICheckNoteTransactionRepository
    {
        public CheckNoteTransactionRepository(EnKolayCariContext context) : base(context) { }
    }
}
