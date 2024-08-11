using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface ISubProjectDetailsService
    {
        Task<List<SubProjectDetailsVm>> GetAllAsync();
        Task<SubProjectDetailsVm> GetByIdAsync(int id);
        Task<SubProjectDetailsVm> InsertAsync(SubProjectDetailsVm model);
        Task<SubProjectDetailsVm> UpdateAsync(SubProjectDetailsVm model);
        Task<SubProjectDetailsVm> DeleteAsync(int id);

        Task<List<SubProjectDetailsVm>> GetByAsync(SubProjectDetailsSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}
