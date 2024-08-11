using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IProjectExtendService
    {
        Task<List<ProjectExtendVm>> GetAllAsync();
        Task<ProjectExtendVm> GetByIdAsync(int id);
        Task<ProjectExtendVm> InsertAsync(ProjectExtendVm model);
        Task<ProjectExtendVm> UpdateAsync(ProjectExtendVm model);
        Task<ProjectExtendVm> DeleteAsync(int id);

        Task<List<ProjectExtendVm>> GetByAsync(ProjectExtendSearchVm filter = null);
    }
}