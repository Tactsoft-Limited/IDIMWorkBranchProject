using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services.User
{
    public interface IMenuService
    {
        Task<IList<MenuVm>> GetAllAsync();
        Task<IList<MenuVm>> GetByApplicationIdAsync(int applicationId);
        Task<MenuVm> GetByIdAsync(int id);
        Task<MenuVm> InsertAsync(MenuVm model);
        Task<MenuVm> UpdateAsync(MenuVm model);
        Task<MenuVm> DeleteAsync(int id);

        Task<IEnumerable<SelectListItem>> GetDropDownAsync(int? selected = 0);
        Task<IList<MenuGroupByVm>> GetAllGroupByApplicationAsync();
    }
}