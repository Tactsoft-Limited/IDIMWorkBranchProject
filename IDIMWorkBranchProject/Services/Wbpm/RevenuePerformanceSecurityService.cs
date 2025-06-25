using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenuePerformanceSecurityService : BaseService<RevenuePerformanceSecurity>, IRevenuePerformanceSecurityService
    {
        public RevenuePerformanceSecurityService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<RevenuePerformanceSecurity> GetByRevenueIdAsync(int id)
        {
            return await _context.RevenuePerformanceSecurities.Where(x => x.RevenueId == id).FirstOrDefaultAsync();
        }
    }
}