using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FurnitureBillPaymentService : BaseService<FurnitureBillPayment>, IFurnitureBillPaymentService
    {
        public FurnitureBillPaymentService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<FurnitureBillPayment> GetByProjectWorkIdAsync(int id)
        {
            return await _context.FurnitureBillPayments.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
        public async Task<List<FurnitureBillPayment>> GetAllByProjectWorkIdAsync(int id)
        {
            return await _context.FurnitureBillPayments.Where(x => x.ProjectWorkId == id).ToListAsync();
        }
    }
}