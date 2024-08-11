using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IProjectStatusService
    {
        Task<List<ProjectStatusVm>> GetAllAsync();
        Task<ProjectStatusVm> GetByIdAsync(int id);
        Task<ProjectStatusVm> InsertAsync(ProjectStatusVm model);
        Task<ProjectStatusVm> UpdateAsync(ProjectStatusVm model);
        Task<ProjectStatusVm> DeleteAsync(int id);

        Task<List<ProjectStatusVm>> GetByAsync(ProjectStatusSearchVm filter = null);
    }
}