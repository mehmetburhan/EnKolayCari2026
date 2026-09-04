using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Inventory;

namespace EnKolayCari2026.Application.Repository.Inventory
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
