using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IBillPaymentService
    {
        Task<List<BillPaymentVm>> GetAllAsync();
        Task<BillPaymentVm> GetByIdAsync(int id);
        Task<BillPaymentVm> InsertAsync(BillPaymentVm model);
        Task<BillPaymentVm> UpdateAsync(BillPaymentVm model);
        Task<BillPaymentVm> DeleteAsync(int id);

        Task<List<BillPaymentVm>> GetByAsync(BillPaymentSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}