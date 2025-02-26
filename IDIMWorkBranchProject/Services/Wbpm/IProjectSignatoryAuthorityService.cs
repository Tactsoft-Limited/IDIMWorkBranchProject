using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IProjectSignatoryAuthorityService : IBaseService<ProjectSignatoryAuthority>
    {
        Task<ProjectSignatoryAuthority> GetByAdpProjectIdAsync(int aDPProjectId);
        Task<object> GetPagedAsync(ProjectSignatoryAuthoritySearchVm model);
    }
}
