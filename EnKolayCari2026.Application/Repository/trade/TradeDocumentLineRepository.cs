using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Application.Repository.Trade
{
    public class TradeDocumentLineRepository : BaseRepository<TradeDocumentLine>, ITradeDocumentLineRepository
    {
        public TradeDocumentLineRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
