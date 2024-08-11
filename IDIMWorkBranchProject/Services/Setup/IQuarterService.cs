using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public interface IQuarterService
    {
        Task<List<QuarterVm>> GetAllAsync();
        Task<QuarterVm> GetByIdAsync(int id);
        Task<QuarterVm> InsertAsync(QuarterVm model);
        Task<QuarterVm> UpdateAsync(QuarterVm model);
        Task<QuarterVm> DeleteAsync(int id);

        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}