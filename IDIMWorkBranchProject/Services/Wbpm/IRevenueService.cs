using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IRevenueService:IBaseService<Revenue>
    {
        Task<object> GetPagedAsync(RevenueSearchVm model);
        Task<string> GetWorkTitle(int? revenueId);
    }
}
