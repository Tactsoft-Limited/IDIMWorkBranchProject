using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public interface IUnitService
    {
        Task<List<UnitVm>> GetAllAsync();
        Task<UnitVm> GetByIdAsync(int id);
        Task<UnitVm> InsertAsync(UnitVm model);
        Task<UnitVm> UpdateAsync(UnitVm model);
        Task<UnitVm> DeleteAsync(int id);

        Task<IEnumerable<SelectListItem>> GetDropDownAsync(int? selected = 0);
        //Task<SelectList> GetSelectAsync(int? selected = 0);
    }
}