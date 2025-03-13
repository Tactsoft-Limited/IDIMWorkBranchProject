using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IProjectWorkStatusService : IBaseService<ProjectWorkStatus>
    {
        Task<ProjectWorkStatus> GetByProjectWorkIdAsync(int id);        
    }
}
