using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IWorkOrderService : IBaseService<WorkOrder>
    {
        Task<WorkOrder> GetByProjectWorkIdAsync(int id);
        Task<object> GetPagedAsync(WorkOrderSearchVm model);
    }
}
