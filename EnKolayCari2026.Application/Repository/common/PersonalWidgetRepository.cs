using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Application.Repository.Common
{
    public class PersonalWidgetRepository : BaseRepository<PersonalWidget>, IPersonalWidgetRepository
    {
        public PersonalWidgetRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
