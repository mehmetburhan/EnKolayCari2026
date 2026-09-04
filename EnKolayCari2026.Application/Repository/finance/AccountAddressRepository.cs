using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Finance;

namespace EnKolayCari2026.Application.Repository.Finance
{
    public class AccountAddressRepository : BaseRepository<AccountAddress>, IAccountAddressRepository
    {
        public AccountAddressRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
