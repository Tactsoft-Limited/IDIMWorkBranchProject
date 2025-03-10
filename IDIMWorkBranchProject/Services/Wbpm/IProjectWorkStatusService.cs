using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IProjectWorkStatusService : IBaseService<ProjectWorkStatus>
    {
        Task<ProjectWorkStatus> GetByProjectWorkIdAsync(int id);
    }
}
