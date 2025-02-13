using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
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
        public async Task<List<ContractAgreement>> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ContractAgreement
                                 .Where(x => x.ProjectWorkId == id)
                                 .ToListAsync(); // Ensure async EF execution
        }
        public async Task<List<ContractAgreement>> GetByContructionFirmIdAsync(int id)
        {
            return await _context.ContractAgreement
                                 .Where(x => x.ConstructionCompanyId == id)
                                 .ToListAsync(); // Ensure async EF execution
        }

        public async Task<string> GetContactDayAsync(int? contracAgreementId)
        {
            return await _context.ContractAgreement.Where(x => x.ContractAgreementId == contracAgreementId).Select(x => x.ContractDay).FirstOrDefaultAsync();
        }
    }
}