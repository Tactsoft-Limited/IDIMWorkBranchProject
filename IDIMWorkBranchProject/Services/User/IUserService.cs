using System.Collections.Generic;
using System.Threading.Tasks;
using IDIMWorkBranchProject.Models;
using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services.User
{
    public interface IUserService
    {
        Task<List<UserVm>> GetAllAsync(bool excludeNotActive = true);

        Task<UserVm> GetByIdAsync(int id, bool checkActive = false);

        Task<UserVm> InsertAsync(UserVm model);

        Task<UserVm> UpdateAsync(UserVm model);

        Task<UserVm> ChangePasswordAsync(ChangePasswordViewModel model);

        Task<UserVm> DeleteAsync(int id);

        Task<UserInformation> LoginAsync(string username, string password, bool? rememberMe = false);

        Task<UserInformation> AdLoginAsync(string username, string password, bool? rememberMe);

        Task<bool> IsOtpValid(int userId, string otp);

        Task SaveOtpAndSendEmail(int userId);
    }
}