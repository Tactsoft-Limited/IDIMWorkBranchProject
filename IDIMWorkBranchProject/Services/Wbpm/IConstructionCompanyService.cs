using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IConstructionCompanyService : IBaseService<ConstructionCompany>
    {
        Task<string> GetConstructionFirm(int constructionCompanyId);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
        Task<object> GetPagedAsync(ConstructionCompanySearchVm model);
    }
}
