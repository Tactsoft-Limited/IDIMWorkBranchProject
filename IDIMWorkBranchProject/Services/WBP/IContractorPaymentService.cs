using IDIMWorkBranchProject.Models.WBP;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IContractorPaymentService
    {
        Task<List<ContractorPaymentVm>> GetAllAsync();
        Task<ContractorPaymentVm> GetByIdAsync(int id);
        Task<ContractorPaymentVm> InsertAsync(ContractorPaymentVm model);
        Task<ContractorPaymentVm> UpdateAsync(ContractorPaymentVm model);
        Task<ContractorPaymentVm> DeleteAsync(int id);

        Task<object> GetByAsync(ContractorPaymentSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}
