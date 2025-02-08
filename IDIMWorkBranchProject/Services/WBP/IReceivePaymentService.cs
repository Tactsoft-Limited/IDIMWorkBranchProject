using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IReceivePaymentService
    {
        Task<List<ReceivePaymentVm>> GetAllAsync();
        Task<ReceivePaymentVm> GetByIdAsync(int id);
        Task<ReceivePaymentVm> InsertAsync(ReceivePaymentVm model);
        Task<ReceivePaymentVm> UpdateAsync(ReceivePaymentVm model);
        Task<ReceivePaymentVm> DeleteAsync(int id);

        Task<List<ReceivePaymentVm>> GetByAsync(ReceivePaymentSearchVm filter = null);
        Task<int> GetTotalPaymentReceiveAsync(int id);
        Task<object> GetAllAsync(ReceivePaymentSearchVm model);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}