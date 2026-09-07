using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnKolayCari.Persistence.Context;
using EnKolayCari.Application.BaseRepository;
using EnKolayCari.Domain.Model.Report;

namespace EnKolayCari.Application.Repository.Report
{
    public class ReportDefRepository : BaseRepository<ReportDef>, IReportDefRepository
    {
        public ReportDefRepository(EnKolayCariContext context) : base(context) { }
    }
}
