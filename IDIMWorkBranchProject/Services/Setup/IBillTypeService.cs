using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public interface IBillTypeService
    {
        Task<List<BillTypeVm>> GetAllAsync();
        Task<BillTypeVm> GetByIdAsync(int id);
        Task<BillTypeVm> InsertAsync(BillTypeVm model);
        Task<BillTypeVm> UpdateAsync(BillTypeVm model);
        Task<BillTypeVm> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}