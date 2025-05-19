using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IRevenuePerformanceSecurityService:IBaseService<RevenuePerformanceSecurity>
    {
        Task<RevenuePerformanceSecurity> GetByRevenueIdAsync(int id);
    }
}
