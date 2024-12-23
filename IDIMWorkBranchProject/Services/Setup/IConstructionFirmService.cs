using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Services.Setup
{
    public interface IConstructionFirmService
    {
        Task<List<ConstructionFirmVm>> GetAllAsync();
        Task<object> GetAllAsync(ConstructionFirmSearchVm model);
        Task<ConstructionFirmVm> GetByIdAsync(int id);
        Task<ConstructionFirmVm> InsertAsync(ConstructionFirmVm constructionFirmVm);
        Task<ConstructionFirmVm> UpdateAsync(ConstructionFirmVm constructionFirmVm);
        Task<ConstructionFirmVm> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
        Task<IEnumerable<SelectListItem>> GetDropdownBySubProjectAsync(int subProjectId);

    }
}