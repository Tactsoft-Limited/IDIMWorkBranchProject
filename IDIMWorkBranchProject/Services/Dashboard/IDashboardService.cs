using IDIMWorkBranchProject.Models.Dashboard;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardVm> GetAll();
        //Task<List<ProjectVm>> ProjectList();
    }
}
