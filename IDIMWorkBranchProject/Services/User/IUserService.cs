using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services.User
{
    public interface IUserService
    {
        Task<List<UserVm>> GetAllAsync(bool excludeNotActive = true);
        Task<UserVm> GetByIdAsync(int id, bool checkActive = false);
        Task<UserVm> InsertAsync(RegisterVm model);
        Task<UserVm> UpdateAsync(UserVm model);
        Task<UserVm> DeleteAsync(int id);

        Task<IList<MenuGroupByVm>> GetUserMenuAsync(int userId);

        Task<IList<ApplicationAssignVm>> GetUserApplicationAsync(int userId);

        Task<List<UserVm>> GetUserByFilterAsync(string username = null, string regiment = null, int? applicationId = null, int? deviceId = null,
            Active active = Active.All);
        Task<UserInformation> LoginAsync(string username, string password, bool? rememberMe = false);
    }
}