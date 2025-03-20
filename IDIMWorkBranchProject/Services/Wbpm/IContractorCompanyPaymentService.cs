using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IContractorCompanyPaymentService : IBaseService<ContractorCompanyPayment>
    {
        Task<List<ContractorCompanyPayment>> GetByProjectWorkIdAsync(int id);
        Task<object> GetPagedAsync(ContractorCompanyPaymentSearchVm model);
    }
}
