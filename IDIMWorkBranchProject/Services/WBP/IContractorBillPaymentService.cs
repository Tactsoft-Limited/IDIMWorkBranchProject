using IDIMWorkBranchProject.Models.WBP;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IContractorBillPaymentService
    {
        Task<List<ContractorBillPaymentVm>> GetAllAsync();
        Task<ContractorBillPaymentVm> GetByIdAsync(int id);
        Task<ContractorBillPaymentVm> InsertAsync(ContractorBillPaymentVm model);
        Task<ContractorBillPaymentVm> UpdateAsync(ContractorBillPaymentVm model);
        Task<ContractorBillPaymentVm> DeleteAsync(int id);

        Task<List<ContractorBillPaymentVm>> GetByAsync(ConsultantSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}
