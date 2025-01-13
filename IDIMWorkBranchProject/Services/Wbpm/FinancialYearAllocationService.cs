using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FinancialYearAllocationService : BaseService<FinancialYearAllocation>, IFinancialYearAllocationService
    {
        public FinancialYearAllocationService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<FinancialYearAllocation>> GetAllByProjectId(int id)
        {
            return await _context.FinancialYearAllocations.Where(x => x.ADPProjectId == id).ToListAsync();
        }
    }
}