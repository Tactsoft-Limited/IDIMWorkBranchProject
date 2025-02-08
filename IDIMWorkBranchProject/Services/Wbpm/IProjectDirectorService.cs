using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IProjectDirectorService : IBaseService<ProjectDirector>
    {
        Task<IEnumerable<SelectListItem>> DropdownAsync(int? selected = 0);
        Task<List<ProjectDirector>> GetAllByProjectId(int id);
        Task<object> GetPagedAsync(ProjectDirectorSearchVm model);
    }
}
