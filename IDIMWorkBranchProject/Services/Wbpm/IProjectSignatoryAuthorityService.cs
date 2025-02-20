using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IProjectSignatoryAuthorityService : IBaseService<ProjectSignatoryAuthority>
    {
        Task<ProjectSignatoryAuthority> GetByAdpProjectIdAsync(int id);
        Task<object> GetPagedAsync(ProjectSignatoryAuthoritySearchVM model);

    }
}
