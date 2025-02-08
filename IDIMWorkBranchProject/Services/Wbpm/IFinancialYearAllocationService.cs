using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IFinancialYearAllocationService : IBaseService<FinancialYearAllocation>
    {
        Task<object> GetPagedAsync(FinancialYearAllocationSearchVm model);
        Task<List<FinancialYearAllocation>> GetAllByProjectId(int id);
    }
}
