using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ProjectWorkStatusService : BaseService<ProjectWorkStatus>, IProjectWorkStatusService
    {
        public ProjectWorkStatusService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<ProjectWorkStatus> GetByProjectWorkIdAsync(int id)
        {
            return await _context.ProjectWorkStatuses.Where(x => x.ProjectWorkId == id).FirstOrDefaultAsync();
        }        
    }
}