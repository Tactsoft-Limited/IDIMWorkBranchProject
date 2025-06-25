using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IRevenueContractAgreementService:IBaseService<RevenueContractAgreement>
    {
        Task<RevenueContractAgreement> GetByRevenueIdAsync(int id);
    }
}
