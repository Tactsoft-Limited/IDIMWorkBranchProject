using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IProjectService
    {
        Task<List<ProjectVm>> GetAllAsync();
        Task<ProjectVm> GetByIdAsync(int id);
        Task<ProjectVm> InsertAsync(ProjectVm model);
        Task<ProjectVm> UpdateAsync(ProjectVm model);
        Task<ProjectVm> DeleteAsync(int id);

        Task<List<ProjectVm>> GetByAsync(ProjectSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}