using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class RevenueContractAgreementService : BaseService<RevenueContractAgreement>,IRevenueContractAgreementService
    {
        public RevenueContractAgreementService(IDIMDBEntities context) : base(context)
        {
        }
        public async Task<RevenueContractAgreement> GetByRevenueIdAsync(int id)
        {
            return await _context.RevenueContractAgreements.Where(x => x.RevenueId == id).FirstOrDefaultAsync();
        }
        
    }
}