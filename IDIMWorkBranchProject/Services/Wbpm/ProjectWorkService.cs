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
    public class ProjectWorkService : BaseService<ProjectWork>, IProjectWorkService
    {
        public ProjectWorkService(IDIMDBEntities context) : base(context)
        {
        }

        public async Task<List<ProjectWork>> GetAllByProjectId(int id)
        {
            return await _context.ProjectWorks.Where(x => x.ADPProjectId == id).ToListAsync();
        }
    }
}