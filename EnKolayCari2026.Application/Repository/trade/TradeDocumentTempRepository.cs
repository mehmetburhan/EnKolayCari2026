using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Application.Repository.Trade
{
    public class TradeDocumentTempRepository : BaseRepository<TradeDocumentTemp>, ITradeDocumentTempRepository
    {
        public TradeDocumentTempRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
