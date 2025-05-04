

using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IRevenueNohaService:IBaseService<RevenueNoha>
    {
        Task<RevenueNoha> GetByProjectWorkIdAsync(int id);


    }
}
