using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ADPReceivePaymentService : BaseService<ADPReceivePayment>, IADPReceivePaymentService
    {

        public ADPReceivePaymentService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<ADPReceivePayment>> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ADPReceivePayments.Where(x=>x.ProjectWorkId==id).ToListAsync();
        }
    }
}