using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface ISubProjectService
    {
        Task<List<SubProjectVm>> GetAllAsync();
        Task<SubProjectVm> GetByIdAsync(int id);
        Task<SubProjectVm> InsertAsync(SubProjectVm model);
        Task<SubProjectVm> UpdateAsync(SubProjectVm model);
        Task<SubProjectVm> DeleteAsync(int id);

        Task<List<SubProjectVm>> GetByAsync(SubProjectSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
        Task<object> GetAllAsync(SubProjectSearchVm filter);
        Task<IEnumerable<SelectListItem>> GetAllFirmByProject(int projectId);
        Task<SubProjectVm> GetByProjectIdAsync(int projectId);
    }
}