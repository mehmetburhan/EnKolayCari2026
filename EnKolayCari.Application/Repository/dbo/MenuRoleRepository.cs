using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Dbo;

namespace EnKolayCari.Application.Repository.Dbo
{
    public class MenuRoleRepository : BaseRepository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(EnKolayCariContext context) : base(context) { }
    }
}
