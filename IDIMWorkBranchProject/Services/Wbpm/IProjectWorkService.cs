using BGB.Data.Entities.Wbpm;

using IDIMWorkBranchProject.Services.Base;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
	public interface IProjectWorkService : IBaseService<ProjectWork>
    {
        Task<List<ProjectWork>> GetAllByProjectId(int id);
    }
}
