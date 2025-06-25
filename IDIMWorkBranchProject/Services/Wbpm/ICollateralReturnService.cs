using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface ICollateralReturnService : IBaseService<CollateralReturn>
    {
        Task<List<CollateralReturn>> GetAllByProjectWorkIdAsync(int id);
        Task<CollateralReturn> GetByProjectWorkIdAsync(int id);
    }
}
