using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Inventory;

namespace EnKolayCari.Application.Repository.Inventory
{
    public class ProductBarcodeRepository : BaseRepository<ProductBarcode>, IProductBarcodeRepository
    {
        public ProductBarcodeRepository(EnKolayCariContext context) : base(context) { }
    }
}
