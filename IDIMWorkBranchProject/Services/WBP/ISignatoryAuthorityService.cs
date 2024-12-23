using IDIMWorkBranchProject.Models.WBP;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Services.WBP
{
    public interface ISignatoryAuthorityService
    {
        Task<List<SignatoryAuthorityVm>> GetAllAsync();
        Task<SignatoryAuthorityVm> GetByIdAsync(int id);
        Task<SignatoryAuthorityVm> InsertAsync(SignatoryAuthorityVm model);
        Task<SignatoryAuthorityVm> UpdateAsync(SignatoryAuthorityVm model);
        Task<SignatoryAuthorityVm> DeleteAsync(int id);

        Task<object> GetByAsync(SignatoryAuthoritySearchVm filter = null);
        Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0);
    }
}
