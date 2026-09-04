using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari2026.Persistence.Context;
using EnKolayCari2026.Application.BaseRepository;
using EnKolayCari2026.Domain.Model.Report;

namespace EnKolayCari2026.Application.Repository.Report
{
    public class ReportDefRepository : BaseRepository<ReportDef>, IReportDefRepository
    {
        public ReportDefRepository(EnKolayCari2026Context context) : base(context) { }
    }
}
