using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class FormalMeetingService : BaseService<FormalMeeting>, IFormalMeetingService
    {
        public FormalMeetingService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<FormalMeeting>> GetAllByProjectId(int id)
        {
            return await _context.FormalMeetings.Where(x => x.ADPProjectId == id).ToListAsync();
        }
    }
}