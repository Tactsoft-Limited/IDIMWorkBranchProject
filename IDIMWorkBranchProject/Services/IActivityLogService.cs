using IDIMWorkBranchProject.Models.User;

namespace IDIMWorkBranchProject.Services
{
    public interface IActivityLogService
    {
        void InsertAsync(ActivityLogVm model);
    }
}