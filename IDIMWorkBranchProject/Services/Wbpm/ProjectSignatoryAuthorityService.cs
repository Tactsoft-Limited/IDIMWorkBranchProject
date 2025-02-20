using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public class ProjectSignatoryAuthorityService : BaseService<ProjectSignatoryAuthority>, IProjectSignatoryAuthorityService 
    {
        public ProjectSignatoryAuthorityService(IDIMDBEntities context) : base(context)
        {
        }


        public async Task<ProjectSignatoryAuthority> GetByAdpProjectIdAsync(int id)
        {
            return await _context.ProjectSignatoryAuthority.Where(x => x.ADPProjectId == id).FirstOrDefaultAsync();
        }
    }
}