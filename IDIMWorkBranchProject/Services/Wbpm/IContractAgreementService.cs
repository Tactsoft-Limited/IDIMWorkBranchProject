using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IContractAgreementService : IBaseService<ContractAgreement>
    {
        Task<ContractAgreement> GetByProjectWorkIdAsync(int id);
        Task<object> GetPagedAsync(ContractAgreementSearchVm model);
    }


}
