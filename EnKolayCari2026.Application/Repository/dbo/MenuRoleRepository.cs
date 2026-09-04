using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Dbo;

namespace EnKolayCari2026.Application.Repository.Dbo
{
    public class MenuRoleRepository : BaseRepository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
