using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class BGBMiscellaneousFundService : BaseService<BGBMiscellaneousFund>, IBGBMiscellaneousFundService
    {
        public BGBMiscellaneousFundService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<BGBMiscellaneousFund> GetByADPPaymentReceiveIdAsync(int id)
        {
            return await _context.BGBMiscellaneousFunds.Where(x => x.ADPReceivePaymentId == id).FirstOrDefaultAsync();
        }

        public async Task<List<BGBMiscellaneousFund>> GetByProjectWorkIdAsync(int projectWorkId)
        {
            return await _context.BGBMiscellaneousFunds.Where(x => x.ProjectWorkId == projectWorkId).ToListAsync();
        }
    }
}