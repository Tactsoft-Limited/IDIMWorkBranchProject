using BGB.Data.Entities.Wbpm;
using BGB.Data.SqlViews.Wbpm;
using BGB.Data.SqlViews.Wbpm.Revenue;
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
        Task<List<ViewFurnitureBillPayment>> GetFurnitureBillPaymentAsync(int id);
        Task<List<ViewCollateralReturn>> GetCollateralReturnAsync(int id);
        Task<List<VatTaxCollateral>> GetVatTaxByProjectIdAsync(int id);
        Task<List<ViewHandoverApproved>> GetHandoverApprovedAsync(int id);
        Task<List<ViewRevenueContractAgreement>> GetRevenueReportAsync(int id);
        Task<List<ViewRevenueWorkOrder>> GetRevenueWorkOrderAsync(int id);
    }
}

