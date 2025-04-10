using BGB.Data.SqlViews.Wbpm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Report
{
    public interface IReportService
    {
        Task<List<ViewADPReceivePayment>> GetADPReceivePaymenAsync(int id);
        Task<List<ViewVatTaxReport>> GetVatTaxAsync(int id);
        Task<List<ViewContactAgreement>> GetContractAgreementAsync(int id);
        Task<List<ViewBGBMiscellaneousFund>> GetBGBMiscellaneousFundAsync(int id);
        Task<List<ViewContractorCompanyPayment>> GetContractorCompanyPaymentAsync(int id);
        Task<List<ViewWorkOrder>> GetWorkOrderAsync(int id);
        Task<List<ViewFinalBillPayment>> GetFinalBillPaymentAsync(int id);
    }
}

