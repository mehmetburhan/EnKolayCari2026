using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Application.Repository.Trade
{
    public class TradeDocumentLineTempRepository : BaseRepository<TradeDocumentLineTemp>, ITradeDocumentLineTempRepository
    {
        public TradeDocumentLineTempRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
