using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Wbpm
{
    public interface IRevenueWorkOrderService:IBaseService<RevenueWorkOrder>
    {
        Task<List<RevenueWorkOrder>> GetAllByRevenueId(int id);
        Task<RevenueWorkOrder> GetByRevenueIdAsync(int id);
    }
}
