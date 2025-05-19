using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IPerformanceSecurityService : IBaseService<PerformanceSecurity>
    {
        Task<PerformanceSecurity> GetByProjectWorkIdAsync(int id);
        Task<object> GetPagedAsync(PerformanceSecuritySearchVm model);
    }
}
