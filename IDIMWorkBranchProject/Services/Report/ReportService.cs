using AutoMapper;

using BGB.Data.SqlViews.Wbpm;

using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Report
{
    public class ReportService : IReportService
    {
        protected readonly IDIMDBEntities _context;
        protected readonly IMapper _mapper;

        public ReportService(IDIMDBEntities context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ViewADPReceivePayment>> GetADPReceivePaymenAsync(int id)
        {
            return await _context.ViewADPReceivePayments.Where(x => x.ADPReceivePaymentId == id).ToListAsync();
        }

        public async Task<List<ViewContactAgreement>> GetContractAgreementAsync(int id)
        {
            return await _context.ViewContactAgreements.Where(x => x.ProjectWorkId == id).ToListAsync();
        }

        public async Task<List<ViewVatTaxReport>> GetVatTaxAsync(int id)
        {
            return await _context.ViewVatTaxReports.Where(x => x.ADPReceivePaymentId == id).ToListAsync();

       
        }
    }
}
