using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface IConsultantService
    {
        Task<List<ConsultantVm>> GetAllAsync();
        Task<ConsultantVm> GetByIdAsync(int id);
        Task<ConsultantVm> InsertAsync(ConsultantVm model);
        Task<ConsultantVm> UpdateAsync(ConsultantVm model);
        Task<ConsultantVm> DeleteAsync(int id);

        Task<List<ConsultantVm>> GetByAsync(ConsultantSearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}
