using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ContractAgreementService : BaseService<ContractAgreement>, IContractAgreementService
    {
        public ContractAgreementService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<ContractAgreement> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ContractAgreements.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }
}