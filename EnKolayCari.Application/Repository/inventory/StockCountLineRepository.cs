using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Inventory;

namespace EnKolayCari.Application.Repository.Inventory
{
    public class StockCountLineRepository : BaseRepository<StockCountLine>, IStockCountLineRepository
    {
        public StockCountLineRepository(EnKolayCariContext context) : base(context) { }
    }
}
