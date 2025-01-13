using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IFinancialYearAllocationService : IBaseService<FinancialYearAllocation>
    {
        Task<List<FinancialYearAllocation>> GetAllByProjectId(int id);
    }
}
