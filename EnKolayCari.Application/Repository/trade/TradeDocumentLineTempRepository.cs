using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Application.Repository.Trade
{
    public class TradeDocumentLineTempRepository : BaseRepository<TradeDocumentLineTemp>, ITradeDocumentLineTempRepository
    {
        public TradeDocumentLineTempRepository(EnKolayCariContext context) : base(context) { }
    }
}
