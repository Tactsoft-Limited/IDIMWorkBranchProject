using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class PerformanceSecurityService : BaseService<PerformanceSecurity>, IPerformanceSecurityService
    {
        public PerformanceSecurityService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<PerformanceSecurity> GetByProjectWorkIdAsync(int id)
        {
            return await _context.PerformanceSecurities.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }
}