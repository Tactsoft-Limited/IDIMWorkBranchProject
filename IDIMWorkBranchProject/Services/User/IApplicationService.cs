using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services.User
{
    public interface IApplicationService
    {
        Task<IList<ApplicationVm>> GetAllAsync();
        Task<ApplicationVm> GetByIdAsync(int id);
        Task<IList<ApplicationVm>> GetByUserIdAsync(int userId);
        Task<ApplicationVm> InsertAsync(ApplicationVm model);
        Task<ApplicationVm> UpdateAsync(ApplicationVm model);
        Task<ApplicationVm> DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetDropDownAsync(int? selected = 0);
    }
}