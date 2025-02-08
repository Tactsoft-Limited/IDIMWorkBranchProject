using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ContractorCompanyPaymentService : BaseService<ContractorCompanyPayment>, IContractorCompanyPaymentService
    {
        public ContractorCompanyPaymentService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<ContractorCompanyPayment>> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ContractorCompanyPayments.Where(x=>x.ProjectWorkId==id).ToListAsync();
        }
    }
}