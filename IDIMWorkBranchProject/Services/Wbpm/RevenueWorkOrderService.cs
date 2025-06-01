using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenueWorkOrderService : BaseService<RevenueWorkOrder>, IRevenueWorkOrderService
    {
        public RevenueWorkOrderService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<List<RevenueWorkOrder>> GetAllByRevenueId(int id)
        {
            return await _context.RevenueWorkOrders.Where(x => x.RevenueId == id).ToListAsync();
        }
        public async Task<RevenueWorkOrder> GetByRevenueIdAsync(int id)
        {
            return await _context.RevenueWorkOrders.Where(x => x.RevenueId == id).FirstOrDefaultAsync();
        }
    }
}