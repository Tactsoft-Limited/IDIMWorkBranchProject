using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Extentions.Collections.Select2;

namespace IDIMWorkBranchProject.Services.Setup
{
    public interface IGeneralInformationService
    {

        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
        Task<Select2PagedResult> GetBySelect2Async(Select2Param param);
    }
}