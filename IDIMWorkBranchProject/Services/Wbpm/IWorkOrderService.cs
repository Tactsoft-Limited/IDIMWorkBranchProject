using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IWorkOrderService : IBaseService<WorkOrder>
    {
        Task<WorkOrder> GetByProjectWorkIdAsync(int id);
        Task<List<WorkOrder>> GetAllByProjectWorkIdAsync(int id);
        Task<object> GetPagedAsync(WorkOrderSearchVm model);
    }
}
