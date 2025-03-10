using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IProjectWorkService : IBaseService<ProjectWork>
    {
        Task<List<ProjectWorkDetailsVm>> GetAllByAdpProjectId(int id);
        Task<object> GetPagedAsync(ProjectWorkSearchVm model);
        Task<string> GetProjectWorkTitle(int? projectWorkId);
    }
}
