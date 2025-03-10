using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class WorkOrderService : BaseService<WorkOrder>, IWorkOrderService
    {
        public WorkOrderService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<WorkOrder> GetByProjectWorkIdAsync(int id)
        {
            return await _context.WorkOrders.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }
}