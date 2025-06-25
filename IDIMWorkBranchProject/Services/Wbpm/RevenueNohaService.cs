using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenueNohaService : BaseService<RevenueNoha>, IRevenueNohaService
    {
        public RevenueNohaService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<RevenueNoha> GetByRevenueIdAsync(int id)
        {
            return await _context.RevenueNohas.Where(x => x.RevenueId == id).FirstOrDefaultAsync();
        }
    }
}