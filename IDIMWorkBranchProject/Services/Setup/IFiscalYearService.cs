using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public interface IFiscalYearService
    {
        Task<List<FiscalYearVm>> GetAllAsync();
        Task<FiscalYearVm> GetByIdAsync(int id);
        Task<FiscalYearVm> InsertAsync(FiscalYearVm model);
        Task<FiscalYearVm> UpdateAsync(FiscalYearVm model);
        Task<FiscalYearVm> DeleteAsync(int id);

        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}