using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IProjectProblemService
    {
        Task<List<ProjectProblemVm>> GetAllAsync();
        Task<ProjectProblemVm> GetByIdAsync(int id);
        Task<ProjectProblemVm> InsertAsync(ProjectProblemVm model);
        Task<ProjectProblemVm> UpdateAsync(ProjectProblemVm model);
        Task<ProjectProblemVm> DeleteAsync(int id);

        Task<List<ProjectProblemVm>> GetByAsync(ProjectProblemSearchVm filter = null);
    }
}