using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Finance;

namespace EnKolayCari.Application.Repository.Finance
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(EnKolayCariContext context) : base(context) { }
    }
}
