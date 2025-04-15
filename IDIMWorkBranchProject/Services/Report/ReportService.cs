using AutoMapper;
using BGB.Data.SqlViews.Wbpm;

using IDIMWorkBranchProject.Data.Database;
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

        public async Task<List<ViewBGBMiscellaneousFund>> GetBGBMiscellaneousFundAsync(int id)
        {
            return await _context.ViewBGBMiscellaneousFunds.Where(x => x.ADPReceivePaymentId == id).ToListAsync();
        }

        public async Task<List<ViewContactAgreement>> GetContractAgreementAsync(int id)
        {
            return await _context.ViewContactAgreements.Where(x => x.ProjectWorkId == id).ToListAsync();
        }

        public async Task<List<ViewVatTaxReport>> GetVatTaxAsync(int id)
        {
            return await _context.ViewVatTaxReports.Where(x => x.ADPReceivePaymentId == id).ToListAsync();

        }
        public async Task<List<ViewContractorCompanyPayment>> GetContractorCompanyPaymentAsync(int id)
        {
            return await _context.ViewContractorCompanyPayments.Where(x => x.ContractorCompanyPaymentId == id).ToListAsync();

        }

        public async Task<List<ViewWorkOrder>> GetWorkOrderAsync(int id)
        {
            return await _context.ViewWorkOrders.Where(x => x.ProjectWorkId == id).ToListAsync();
        }
        public async Task<List<ViewFinalBillPayment>> GetFinalBillPaymentAsync(int id)
        {
            return await _context.ViewFinalBillPayments
                .Where(x => x.FinalBillPaymentId == id)
                .ToListAsync();
        }
    }
}
