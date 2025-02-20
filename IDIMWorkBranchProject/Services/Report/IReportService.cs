using BGB.Data.SqlViews.Wbpm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Report
{
    public interface IReportService
    {
        Task<List<ViewADPReceivePayment>> GetADPReceivePaymenAsync(int id);
        Task<List<ViewContactAgreement>> GetContractAgreementAsync(int id);
    }
}

