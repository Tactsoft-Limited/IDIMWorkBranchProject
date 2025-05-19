using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class HandoverApprovedService : BaseService<HandoverApproved>, IHandoverApprovedService
    {
        public HandoverApprovedService(IDIMDBEntities context) : base(context)
        {
        
        }
        public async Task<HandoverApproved> GetByProjectWorkIdAsync(int id)
        {
            return await _context.HandoverApproveds.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }
    }
}