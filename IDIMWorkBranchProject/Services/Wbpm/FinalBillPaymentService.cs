using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FinalBillPaymentService : BaseService<FinalBillPayment>, IFinalBillPaymentService
    {
        public FinalBillPaymentService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<List<FinalBillPayment>> GetAllByProjectWorkIdAsync(int id)
        {
            return await _context.FinalBillPayments.Where(x => x.ProjectWorkId == id).ToListAsync();
        }
        public async Task<FinalBillPayment> GetByProjectWorkIdAsync(int id)
        {
            return await _context.FinalBillPayments.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }


    }
}