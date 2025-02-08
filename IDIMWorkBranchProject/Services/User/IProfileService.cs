using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.User
{
    public interface IProfileService
    {
        Task<IList<MenuGroupByVm>> GetMenuByUserIdAsync(int userId);

        Task<GeneralInformationVm> GetRegimentInfoByUserId(int? id);

        Task<IList<ApplicationVm>> GetApplicationsByUserId(int userId);
    }
}