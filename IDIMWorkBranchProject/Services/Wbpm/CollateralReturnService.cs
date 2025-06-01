using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class CollateralReturnService : BaseService<CollateralReturn>, ICollateralReturnService
    {
        public CollateralReturnService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<CollateralReturn>> GetAllByProjectWorkIdAsync(int id)
        {
            return await _context.CollateralReturns.Where(x => x.ProjectWorkId == id).ToListAsync();
        }

        public async Task<CollateralReturn> GetByProjectWorkIdAsync(int id)
        {
            return await _context.CollateralReturns.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }

}